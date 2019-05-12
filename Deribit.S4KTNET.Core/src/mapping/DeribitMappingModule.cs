using Autofac;

namespace Deribit.S4KTNET.Core.Mapping
{
    class DeribitMappingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitMappingService.Module>();
        }
    }
}
