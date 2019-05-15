﻿using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Newtonsoft.Json;
using System.Linq;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    public interface IDeribitSubscriptionManagementService
    {
        //------------------------------------------------------------------------------------------------
        // subscription streams
        //------------------------------------------------------------------------------------------------
        IObservable<AnnouncementsNotification> AnnouncementsStream { get; }
        IObservable<BookDepthLimitedNotification> BookDepthLimitedStream { get; }
        IObservable<BookFullNotification> BookFullStream { get; }
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
        // streams
        //------------------------------------------------------------------------------------------------

        public IObservable<AnnouncementsNotification> AnnouncementsStream => AnnouncementsSubject;
        private readonly ISubject<AnnouncementsNotification> AnnouncementsSubject = new Subject<AnnouncementsNotification>();

        public IObservable<BookDepthLimitedNotification> BookDepthLimitedStream => BookDepthLimitedSubject;
        private readonly ISubject<BookDepthLimitedNotification> BookDepthLimitedSubject = new Subject<BookDepthLimitedNotification>();

        public IObservable<BookFullNotification> BookFullStream => BookFullSubject;
        private readonly ISubject<BookFullNotification> BookFullSubject = new Subject<BookFullNotification>();

        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcProxy rpcproxy;
        private readonly StreamJsonRpc.JsonRpc jsonrpc;

        //------------------------------------------------------------------------------------------------
        // fields
        //------------------------------------------------------------------------------------------------

        private readonly JsonSerializer jsonser = new JsonSerializer();

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

        private void handle_notification(object sender, JToken e)
        {
            lock (this) // REMOVE THIS!
            {
                // get channel name
                string channel = (string) e["channel"];
                // validate
                if (string.IsNullOrEmpty(channel))
                    throw new Exception();
                // switch channel
                if (channel.StartsWith(DeribitChannelPrefix.announcements))
                {
                    // deserialize
                    var dto = e.ToObject<AnnouncementsNotificationDto>(jsonser);
                    // map
                    var noti = this.mapper.Map<AnnouncementsNotification>(dto);
                    // validate
                    new AnnouncementsNotification.Validator().ValidateAndThrow(noti);
                    // raise
                    this.AnnouncementsSubject.OnNext(noti);
                }
                else if (channel.StartsWith(DeribitChannelPrefix.book))
                {
                    // depth limited orderbook
                    if (channel.Count(c => c == '.') == 4)
                    {
                        // deserialize
                        var dto = e.ToObject<BookDepthLimitedNotificationDto>(jsonser);
                        // map
                        var noti = this.mapper.Map<BookDepthLimitedNotification>(dto);
                        // validate
                        new BookDepthLimitedNotification.Validator().ValidateAndThrow(noti);
                        // raise
                        this.BookDepthLimitedSubject.OnNext(noti);
                    }
                    // full orderbook
                    else if (channel.Count(c => c == '.') == 2)
                    {
                        // deserialize
                        var dto = e.ToObject<BookFullNotificationDto>(jsonser);
                        // map
                        var noti = this.mapper.Map<BookFullNotification>(dto);
                        // validate
                        new BookFullNotification.Validator().ValidateAndThrow(noti);
                        // raise
                        this.BookFullSubject.OnNext(noti);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        //------------------------------------------------------------------------------------------------

    }
}