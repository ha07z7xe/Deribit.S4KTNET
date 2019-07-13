using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Authentication
{
    public interface IDeribitAuthenticationService
    {
        bool IsAuthenticated { get; }

        Task<AuthResponse> Auth(AuthRequest request, CancellationToken ct = default);
        Task Logout(CancellationToken ct = default);
    }

    internal class DeribitAuthenticationService : IDeribitAuthenticationService
    {
        //------------------------------------------------------------------------------------------------
        // properties
        //------------------------------------------------------------------------------------------------

        public bool IsAuthenticated { get; private set; }

        //-----------------------------------------------------------------------------------------
        // configuration
        //-----------------------------------------------------------------------------------------

        private readonly int RefreshAuthTokenLoopPeriodMins = 15;

        //-----------------------------------------------------------------------------------------
        // loops
        //-----------------------------------------------------------------------------------------

        private readonly System.Timers.Timer RefreshAuthTokenTimer;

        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcService jsonrpc;

        //------------------------------------------------------------------------------------------------
        // credentials
        //------------------------------------------------------------------------------------------------

        private AuthResponse LastAuthResponse;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitAuthenticationService(DeribitService deribit, IMapper mapper,
            IDeribitJsonRpcService jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.jsonrpc = jsonrpc;

            //---------------------------------------------------------------------
            // refresh auth token periodically
            {
                this.RefreshAuthTokenTimer = new System.Timers.Timer()
                {
                    Interval = TimeSpan.FromMinutes(this.RefreshAuthTokenLoopPeriodMins).TotalMilliseconds,
                    Enabled = !this.deribit.deribitconfig.NoRefreshAuthToken,
                };
            }
            this.RefreshAuthTokenTimer.Elapsed += async (sender, e) =>
            {
                if (this.deribit.deribitconfig.NoRefreshAuthToken)
                    return;
                await this.RefreshAuthTokenLoop();
            };
            //---------------------------------------------------------------------
        }

        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitAuthenticationService>()
                    .AsSelf()
                    .As<IDeribitAuthenticationService>()
                    .SingleInstance();
            }
        }

        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            this.RefreshAuthTokenTimer.Stop();
        }

        //------------------------------------------------------------------------------------------------
        // refresh auth token loop
        //------------------------------------------------------------------------------------------------

        private async Task RefreshAuthTokenLoop()
        {
            if (this.LastAuthResponse == null || this.LastAuthResponse.refresh_token == null)
                return;
            // form request
            AuthRequest authrequest = new AuthRequest()
            {
                grant_type = GrantType.refresh_token,
                refresh_token = this.LastAuthResponse.refresh_token,
            };
            // execute
            await this.Auth(authrequest, default);
        }

        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------

        public async Task<AuthResponse> Auth(AuthRequest request, CancellationToken ct)
        {
            // validate
            new AuthRequest.Validator().ValidateAndThrow(request);
            // map request
            AuthRequestDto requestdto = mapper.Map<AuthRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.auth
            (
                grant_type: requestdto.grant_type,
                username: requestdto.username,
                password: requestdto.password,
                client_id: requestdto.client_id,
                client_secret: requestdto.client_secret,
                refresh_token: requestdto.refresh_token,
                timestamp: requestdto.timestamp,
                signature: requestdto.signature,
                nonce: requestdto.nonce,
                state: requestdto.state,
                scope: requestdto.scope,
                ct
            );
            // map response
            AuthResponse response = mapper.Map<AuthResponse>(responsedto);
            // validate
            new AuthResponse.Validator().ValidateAndThrow(response);
            this.IsAuthenticated = true;
            // return
            return LastAuthResponse = response;
        }

        public async Task Logout(CancellationToken ct = default)
        {
            // validate
            //new LogoutRequest.Validator().ValidateAndThrow(request);
            // map request
            //LogoutRequestDto requestdto = mapper.Map<LogoutRequestDto>(request);
            // execute request
            try
            {
                await this.jsonrpc.RpcProxy.logout(ct);
            }
            // ignore expected exception
            catch (StreamJsonRpc.ConnectionLostException)
            {

            }
            //// map response
            //AuthResponse response = mapper.Map<AuthResponse>(responsedto);
            //// return
            //return response;
            this.IsAuthenticated = false;
        }

        //------------------------------------------------------------------------------------------------
    }
}
