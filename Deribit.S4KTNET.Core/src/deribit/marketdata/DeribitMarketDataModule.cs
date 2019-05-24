using Autofac;

namespace Deribit.S4KTNET.Core.MarketData
{
    internal class DeribitMarketDataModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitMarketDataService.Module>();
        }
    }
}
