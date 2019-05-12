using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    public interface IDeribitSubscriptionManagementService
    {
        //------------------------------------------------------------------------------------------------
        // subscription streams
        //------------------------------------------------------------------------------------------------
        event EventHandler<SubscriptionNotificationDto> subscription;
        //------------------------------------------------------------------------------------------------
        // subscribe / unsubscribe
        //------------------------------------------------------------------------------------------------
        Task<SubscribeResponse> subscribe_public(SubscribeRequest request, CancellationToken ct = default);
        Task<UnsubscribeResponse> unsubscribe_public(UnsubscribeRequest request, CancellationToken ct = default);
        Task<SubscribeResponse> subscribe_private(SubscribeRequest request, CancellationToken ct = default);
        Task<UnsubscribeResponse> unsubscribe_private(UnsubscribeRequest request, CancellationToken ct = default);
        //------------------------------------------------------------------------------------------------
    }

    internal class DeribitSubscriptionManagementService : IDeribitSubscriptionManagementService, IDisposable
    {
        //------------------------------------------------------------------------------------------------
        // events
        //------------------------------------------------------------------------------------------------

        public event EventHandler<SubscriptionNotificationDto> subscription;

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

        public DeribitSubscriptionManagementService(DeribitService deribit, IMapper mapper,
            IDeribitJsonRpcProxy rpcproxy, StreamJsonRpc.JsonRpc jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.rpcproxy = rpcproxy;
            this.jsonrpc = jsonrpc;
            this.rpcproxy.subscription += this.handle_notification;
        }

        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitSubscriptionManagementService>()
                    .AsSelf()
                    .As<IDeribitSubscriptionManagementService>()
                    .SingleInstance();
            }
        }


        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            this.rpcproxy.subscription -= this.handle_notification;
        }

        //------------------------------------------------------------------------------------------------
        // subscribe api
        //------------------------------------------------------------------------------------------------

        public async Task<SubscribeResponse> subscribe_public(SubscribeRequest request, CancellationToken ct)
        {
            // validate
            new SubscribeRequest.Validator().ValidateAndThrow(request);
            // map request
            SubscribeRequestDto requestdto = mapper.Map<SubscribeRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.subscribe_public
            (
                channels: requestdto.channels,
                ct
            );
            // map response
            SubscribeResponse response = mapper.Map<SubscribeResponse>(responsedto);
            // return
            return response;
        }

        public async Task<UnsubscribeResponse> unsubscribe_public(UnsubscribeRequest request, CancellationToken ct)
        {
            // validate
            new UnsubscribeRequest.Validator().ValidateAndThrow(request);
            // map request
            UnsubscribeRequestDto requestdto = mapper.Map<UnsubscribeRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.unsubscribe_public
            (
                channels: requestdto.channels,
                ct
            );
            // map response
            UnsubscribeResponse response = mapper.Map<UnsubscribeResponse>(responsedto);
            // return
            return response;
        }

        public async Task<SubscribeResponse> subscribe_private(SubscribeRequest request, CancellationToken ct)
        {
            // validate
            new SubscribeRequest.Validator().ValidateAndThrow(request);
            // map request
            SubscribeRequestDto requestdto = mapper.Map<SubscribeRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.subscribe_private
            (
                channels: requestdto.channels,
                ct
            );
            // map response
            SubscribeResponse response = mapper.Map<SubscribeResponse>(responsedto);
            // return
            return response;
        }

        public async Task<UnsubscribeResponse> unsubscribe_private(UnsubscribeRequest request, CancellationToken ct)
        {
            // validate
            new UnsubscribeRequest.Validator().ValidateAndThrow(request);
            // map request
            UnsubscribeRequestDto requestdto = mapper.Map<UnsubscribeRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.unsubscribe_private
            (
                channels: requestdto.channels,
                ct
            );
            // map response
            UnsubscribeResponse response = mapper.Map<UnsubscribeResponse>(responsedto);
            // return
            return response;
        }

        //------------------------------------------------------------------------------------------------
        // notification handling
        //------------------------------------------------------------------------------------------------

        private void handle_notification(object sender, SubscriptionNotificationDto e)
        {
            ;
        }

        //------------------------------------------------------------------------------------------------

    }
}
