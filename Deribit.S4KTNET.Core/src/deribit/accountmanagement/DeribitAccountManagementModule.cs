using Autofac;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    internal class DeribitAccountManagementModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitAccountManagementService.Module>();
        }
    }
}
