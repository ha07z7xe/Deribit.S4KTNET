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
        //------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------

        private readonly IDeribitService deribit;

        //------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------
        public DeribitAuthenticationService(IDeribitService deribit)
        {
            this.deribit = deribit;
        }

        //------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------

        public Task<AuthResponse> Auth(AuthRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Logout(LogoutRequest request)
        {
            throw new NotImplementedException();
        }

        //------------------------------------------------------------------------
    }
}
