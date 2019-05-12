using System;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.SessionManagement;
using Deribit.S4KTNET.Core.Supporting;

namespace Deribit.S4KTNET.Core
{
    public interface IDeribitService : IDisposable
    {
        IDeribitAuthenticationService Authentication { get; }

        IDeribitSessionManagementService SessionManagement { get; }

        IDeribitSupportingService Supporting { get; }
    }

    public class DeribitService : IDeribitService
    {
        //------------------------------------------------------------------------
        // sub services
        //------------------------------------------------------------------------

        public IDeribitAuthenticationService Authentication { get; }

        public IDeribitSessionManagementService SessionManagement { get; }

        public IDeribitSupportingService Supporting { get; }

        //------------------------------------------------------------------------
        // components
        //------------------------------------------------------------------------

        private readonly Serilog.ILogger logger;

        //------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------

        public DeribitService()
        {
            this.logger = Serilog.Log.ForContext<DeribitService>();
            this.Authentication = new DeribitAuthenticationService(this);
            this.SessionManagement = new DeribitSessionManagementService(this);
            this.Supporting = new DeribitSupportingService(this);
        }

        //------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------

        public void Dispose()
        {

        }

        //------------------------------------------------------------------------
    }
}
