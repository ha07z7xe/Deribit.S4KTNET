using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Deribit.S4KTNET.Core.WebSocket
{
    class DeribitWebSocketModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitWebSocketService.Module>();
        }
    }
}
