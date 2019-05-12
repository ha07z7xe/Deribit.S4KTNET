using Autofac;

namespace Deribit.S4KTNET.Core.Authentication
{
    internal class DeribitAuthenticationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DeribitAuthenticationService.Module>();
        }
    }
}
