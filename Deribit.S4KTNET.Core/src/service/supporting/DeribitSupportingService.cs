using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Supporting
{
    public interface IDeribitSupportingService
    {
        Task<long> GetTime();
    }

    internal class DeribitSupportingService : IDeribitSupportingService
    {
        //------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------

        private readonly IDeribitService deribit;

        //------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------

        public DeribitSupportingService(IDeribitService deribit)
        {
            this.deribit = deribit;
        }

        //------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------


        public Task<long> GetTime()
        {
            throw new NotImplementedException();
        }
        //------------------------------------------------------------------------

    }
}
