using Autofac;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Authentication
{
    public interface IDeribitAuthenticationService
    {
        Task<AuthResponse> Auth(AuthRequest request);
        Task Logout(LogoutRequest request);
    }

    internal class DeribitAuthenticationService : IDeribitAuthenticationService
    {
        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly IDeribitService deribit;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------
        public DeribitAuthenticationService(IDeribitService deribit)
        {
            this.deribit = deribit;
        }

        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitAuthenticationService>()
                    .AsSelf()
                    .As<IDeribitAuthenticationService>()
                    .SingleInstance();
            }
        }

        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------

        public Task<AuthResponse> Auth(AuthRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Logout(LogoutRequest request)
        {
            throw new NotImplementedException();
        }

        //------------------------------------------------------------------------------------------------
    }
}
