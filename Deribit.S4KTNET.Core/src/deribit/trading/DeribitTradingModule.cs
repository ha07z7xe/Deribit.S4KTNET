using Autofac;

namespace Deribit.S4KTNET.Core.Trading
{
    internal class DeribitTradingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitTradingService.Module>();
        }
    }
}
