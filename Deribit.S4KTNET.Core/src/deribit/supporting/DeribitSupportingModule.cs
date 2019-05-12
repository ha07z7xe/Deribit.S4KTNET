using Autofac;

namespace Deribit.S4KTNET.Core.Supporting
{
    internal class DeribitSupportingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitSupportingService.Module>();
        }
    }
}
