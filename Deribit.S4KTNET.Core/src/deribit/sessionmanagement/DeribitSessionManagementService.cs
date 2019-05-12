using Autofac;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    public interface IDeribitSessionManagementService
    {
        Task SetHeartbeat(SetHeartbeatRequest request);

        Task DisableHeartbeat(DisableHeartbeatRequest request);
    }

    internal class DeribitSessionManagementService : IDeribitSessionManagementService
    {
        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly IDeribitService deribit;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitSessionManagementService(IDeribitService deribit)
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
                builder.RegisterType<DeribitSessionManagementService>()
                    .AsSelf()
                    .As<IDeribitSessionManagementService>()
                    .SingleInstance();
            }
        }


        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------

        public Task DisableHeartbeat(DisableHeartbeatRequest request)
        {
            throw new NotImplementedException();
        }

        public Task SetHeartbeat(SetHeartbeatRequest request)
        {
            throw new NotImplementedException();
        }

        //------------------------------------------------------------------------------------------------

    }
}
