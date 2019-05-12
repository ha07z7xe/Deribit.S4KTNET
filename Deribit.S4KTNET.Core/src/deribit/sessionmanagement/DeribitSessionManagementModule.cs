using Autofac;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    internal class DeribitSessionManagementModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitSessionManagementService.Module>();
        }
    }
}
