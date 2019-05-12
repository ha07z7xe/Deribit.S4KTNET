using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Supporting
{
    public interface IDeribitSupportingService
    {
        Task<long> GetTime();
    }

    public class DeribitSupportingService : IDeribitSupportingService
    {
        //----------------------------------------------------------
        public DeribitSupportingService()
        {

        }
        //----------------------------------------------------------
        public Task<long> GetTime()
        {
            throw new NotImplementedException();
        }
        //----------------------------------------------------------
    }
}
