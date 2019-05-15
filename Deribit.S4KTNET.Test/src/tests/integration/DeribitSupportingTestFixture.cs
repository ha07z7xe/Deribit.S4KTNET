using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Supporting;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Supporting
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class SupportingTestFixture
    {
        //----------------------------------------------------------------------------
        // components
        //----------------------------------------------------------------------------

        private DeribitConfig deribitconfig;
        private DeribitService deribit;

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
        // public/test
        //----------------------------------------------------------------------------

        [Test]
        [Description("public/test")]
        public async Task Test_test_success()
        {
            TestResponse testresponse = await deribit.Supporting.test(new TestRequest()
            {
                expected_result = null,
            });
            Assert.That(testresponse.version, Is.Not.Null);
        }

        [Test]
        [Description("public/test")]
        [Ignore("Breaks the websocket connection")]
        public async Task Test_test_error()
        {
            Assert.ThrowsAsync<RemoteInvocationException>(async () =>
            {
                TestResponse testresponse = await deribit.Supporting.test(new TestRequest()
                {
                    expected_result = "exception",
                });
            });
        }

        //----------------------------------------------------------------------------
        // public/get_time
        //----------------------------------------------------------------------------

        [Test]
        [Description("public/test")]
        public async Task Test_gettime()
        {
            GetTimeResponse timeresponse = await deribit.Supporting.get_time();
            var timedifferencemillis = Math.Abs((DateTime.UtcNow - timeresponse.server_time).TotalMilliseconds);
            Assert.That(timedifferencemillis, Is.LessThan(1000));
        }

        //----------------------------------------------------------------------------
        // public/hello
        //----------------------------------------------------------------------------

        [Test]
        [Description("public/hello")]
        public async Task Test_hello()
        {
            HelloResponse helloresponse = await deribit.Supporting.hello(new HelloRequest
            {
                client_name = "clientname",
                client_version = "1",
            });
            Assert.That(helloresponse.version, Is.Not.Null);
        }

        //----------------------------------------------------------------------------
    }
}
