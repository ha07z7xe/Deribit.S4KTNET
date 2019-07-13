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

        Task<GenericResponse> DisableCancelOnDisconnect(CancellationToken ct = default);
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
        private readonly IDeribitJsonRpcService jsonrpc;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitSessionManagementService(DeribitService deribit, IMapper mapper,
            IDeribitJsonRpcService jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.jsonrpc = jsonrpc;
            this.jsonrpc.ReconnectionHappened += this.Jsonrpc_ReconnectionHappened;
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
            this.jsonrpc.RpcProxy.heartbeat -= this.handle_heartbeat;
        }

        //------------------------------------------------------------------------------------------------
        // reconnection
        //------------------------------------------------------------------------------------------------

        private void Jsonrpc_ReconnectionHappened(ReconnectionType obj)
        {
            this.jsonrpc.RpcProxy.heartbeat += this.handle_heartbeat;
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
            var responsedto = await this.jsonrpc.RpcProxy.set_heartbeat(requestDto.interval, ct);
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
            var responsedto = await this.jsonrpc.RpcProxy.disable_heartbeat(ct);
            // map response
            GenericResponse response = mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> EnableCancelOnDisconnect(CancellationToken ct = default)
        {
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.enable_cancel_on_disconnect(ct);
            // map response
            GenericResponse response = mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> DisableCancelOnDisconnect(CancellationToken ct = default)
        {
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.disable_cancel_on_disconnect(ct);
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
                _ = deribit.Supporting.Test(new Supporting.TestRequest());
            }
            // raise
            this.HeartbeatSubject.OnNext(new Unit());
        }

        //------------------------------------------------------------------------------------------------

    }
}
