using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using Newtonsoft.Json.Linq;
using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    public interface IDeribitSessionManagementService
    {
        IObservable<Unit> HeartbeatStream { get; }

        Task<GenericResponse> SetHeartbeat(SetHeartbeatRequest request, CancellationToken ct = default);

        Task<GenericResponse> DisableHeartbeat(CancellationToken ct = default);

        Task<GenericResponse> EnableCancelOnDisconnect(CancellationToken ct = default);

        Task<GenericResponse> disable_cancel_on_disconnect(CancellationToken ct = default);
    }

    internal class DeribitSessionManagementService : IDeribitSessionManagementService
    {
        //------------------------------------------------------------------------------------------------
        // streams
        //------------------------------------------------------------------------------------------------

        public IObservable<Unit> HeartbeatStream => HeartbeatSubject;
        private readonly ISubject<Unit> HeartbeatSubject = new Subject<Unit>();

        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcProxy rpcproxy;
        private readonly StreamJsonRpc.JsonRpc jsonrpc;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitSessionManagementService(DeribitService deribit, IMapper mapper,
            IDeribitJsonRpcProxy rpcproxy, StreamJsonRpc.JsonRpc jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.rpcproxy = rpcproxy;
            this.jsonrpc = jsonrpc;
            this.rpcproxy.heartbeat += this.handle_heartbeat;
        }

        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitSessionManagementService>()
                    .AsSelf()
                    .As<IDeribitSessionManagementService>()
                    .SingleInstance();
            }
        }

        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            this.rpcproxy.heartbeat -= this.handle_heartbeat;
        }

        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------

        public async Task<GenericResponse> SetHeartbeat(SetHeartbeatRequest request, CancellationToken ct)
        {
            // validate
            new SetHeartbeatRequest.Validator().ValidateAndThrow(request);
            // map request
            SetHeartbeatRequestDto requestDto = mapper.Map<SetHeartbeatRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.set_heartbeat(requestDto.interval, ct);
            // map response
            GenericResponse response = mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> DisableHeartbeat(CancellationToken ct)
        {
            // validate
            //new DisableHeartbeatRequest.Validator().ValidateAndThrow(request);
            // map request
            //DisableHeartbeatRequestDto requestDto = mapper.Map<DisableHeartbeatRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.disable_heartbeat(ct);
            // map response
            GenericResponse response = mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> EnableCancelOnDisconnect(CancellationToken ct = default)
        {
            // execute request
            var responsedto = await this.rpcproxy.enable_cancel_on_disconnect(ct);
            // map response
            GenericResponse response = mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> disable_cancel_on_disconnect(CancellationToken ct = default)
        {
            // execute request
            var responsedto = await this.rpcproxy.disable_cancel_on_disconnect(ct);
            // map response
            GenericResponse response = mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        //------------------------------------------------------------------------------------------------
        // heartbeat handling
        //------------------------------------------------------------------------------------------------

        private void handle_heartbeat(object sender, JToken e)
        {
            // response to heartbeat
            if (!this.deribit.deribitconfig.NoRespondHeartbeats)
            {
                deribit.Supporting.Test(new Supporting.TestRequest());
            }
            // raise
            this.HeartbeatSubject.OnNext(new Unit());
        }

        //------------------------------------------------------------------------------------------------

    }
}
