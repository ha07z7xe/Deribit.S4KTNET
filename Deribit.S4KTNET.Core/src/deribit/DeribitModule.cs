using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.JsonRpc;
using Deribit.S4KTNET.Core.Mapping;
using Deribit.S4KTNET.Core.SessionManagement;
using Deribit.S4KTNET.Core.AccountManagement;
using Deribit.S4KTNET.Core.SubscriptionManagement;
using Deribit.S4KTNET.Core.Supporting;
using Deribit.S4KTNET.Core.Trading;
using Deribit.S4KTNET.Core.WebSocket;

namespace Deribit.S4KTNET.Core
{
    internal class DeribitModule : Autofac.Module
    {
        private readonly DeribitService deribit;

        public DeribitModule(DeribitService deribit)
        {
            this.deribit = deribit;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DeribitService.Module(deribit));

            builder.RegisterModule<DeribitWebSocketModule>();
            builder.RegisterModule<DeribitJsonRpcModule>();
            builder.RegisterModule<DeribitMappingModule>();

            builder.RegisterModule<DeribitAuthenticationModule>();
            builder.RegisterModule<DeribitAccountManagementModule>();
            builder.RegisterModule<DeribitSessionManagementModule>();
            builder.RegisterModule<DeribitSupportingModule>();
            builder.RegisterModule<DeribitSubscriptionManagementModule>();
            builder.RegisterModule<DeribitTradingModule>();
        }
    }
}
