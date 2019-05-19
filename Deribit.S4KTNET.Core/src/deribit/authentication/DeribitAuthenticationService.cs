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
        Task<AuthResponse> Auth(AuthRequest request, CancellationToken ct = default);
        Task Logout(LogoutRequest request, CancellationToken ct = default);
    }

    internal class DeribitAuthenticationService : IDeribitAuthenticationService
    {
        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcProxy rpcproxy;
        private readonly StreamJsonRpc.JsonRpc jsonrpc;

        //------------------------------------------------------------------------------------------------
        // credentials
        //------------------------------------------------------------------------------------------------

        private AuthResponse LastAuthResponse;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitAuthenticationService(DeribitService deribit, IMapper mapper,
            IDeribitJsonRpcProxy rpcproxy, StreamJsonRpc.JsonRpc jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.rpcproxy = rpcproxy;
            this.jsonrpc = jsonrpc;
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
        // api
        //------------------------------------------------------------------------------------------------

        public async Task<AuthResponse> Auth(AuthRequest request, CancellationToken ct)
        {
            // validate
            new AuthRequest.Validator().ValidateAndThrow(request);
            // map request
            AuthRequestDto requestdto = mapper.Map<AuthRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.auth
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
            // return
            return LastAuthResponse = response;
        }

        public async Task Logout(LogoutRequest request, CancellationToken ct = default)
        {
            // validate
            //new LogoutRequest.Validator().ValidateAndThrow(request);
            // map request
            LogoutRequestDto requestdto = mapper.Map<LogoutRequestDto>(request);
            // execute request
            try
            {
                await this.rpcproxy.logout(ct);
            }
            // ignore expected exception
            catch (StreamJsonRpc.ConnectionLostException)
            {

            }
            //// map response
            //AuthResponse response = mapper.Map<AuthResponse>(responsedto);
            //// return
            //return response;
        }

        //------------------------------------------------------------------------------------------------
    }
}
