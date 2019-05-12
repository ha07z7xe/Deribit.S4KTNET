using System;

namespace Deribit.S4KTNET.Core
{
    public interface IDeribitService : IDisposable
    {
        IDeribitAuthenticationService AuthenticationService { get; }
    }

    public class DeribitService : IDeribitService
    {
        //------------------------------------------------------------------------
        // sub services
        //------------------------------------------------------------------------

        public IDeribitAuthenticationService AuthenticationService { get; }

        //------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------

        public DeribitService()
        {

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
