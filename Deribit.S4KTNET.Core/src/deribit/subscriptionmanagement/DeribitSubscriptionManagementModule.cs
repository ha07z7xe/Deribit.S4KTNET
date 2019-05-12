using Autofac;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    internal class DeribitSubscriptionManagementModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitSubscriptionManagementService.Module>();
        }
    }
}
