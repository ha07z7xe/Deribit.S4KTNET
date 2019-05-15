using Deribit.S4KTNET.Core;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    abstract class DeribitIntegrationTestFixtureBase
    {
        //----------------------------------------------------------------------------
        // components
        //----------------------------------------------------------------------------

        protected DeribitConfig deribitconfig;
        protected DeribitService deribit;

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            // create config
            deribitconfig = new DeribitConfig
            {
                Environment = DeribitEnvironment.Test,
                EnableJsonRpcTracing = true,
            };

            // construct services
            deribit = new DeribitService(deribitconfig);

            // connect
            await deribit.Connect(default);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            deribit.Dispose();
        }


        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        //----------------------------------------------------------------------------
    }
}
