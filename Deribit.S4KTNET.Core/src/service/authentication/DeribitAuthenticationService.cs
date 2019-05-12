using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Authentication
{
    public interface IDeribitAuthenticationService
    {
        Task<AuthResponse> Auth(AuthRequest request);
        Task Logout(LogoutRequest request);
    }

    public class DeribitAuthenticationService : IDeribitAuthenticationService
    {
        //----------------------------------------------------------
        public DeribitAuthenticationService()
        {

        }
        //----------------------------------------------------------
        public Task<AuthResponse> Auth(AuthRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Logout(LogoutRequest request)
        {
            throw new NotImplementedException();
        }
        //----------------------------------------------------------
    }
}
