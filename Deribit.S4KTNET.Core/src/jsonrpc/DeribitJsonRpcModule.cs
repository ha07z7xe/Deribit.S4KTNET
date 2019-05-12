using Autofac;

namespace Deribit.S4KTNET.Core.JsonRpc
{
    internal class DeribitJsonRpcModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitJsonRpcService.Module>();
        }
    }
}
