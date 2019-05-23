using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Supporting;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class SupportingTestFixture : DeribitIntegrationTestFixtureBase
    {
        //----------------------------------------------------------------------------
        // public/test
        //----------------------------------------------------------------------------

        [Test]
        [Description("public/test")]
        public async Task Test_test_success()
        {
            TestResponse testresponse = await deribit.Supporting.Test(new TestRequest()
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
                TestResponse testresponse = await deribit.Supporting.Test(new TestRequest()
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
            GetTimeResponse timeresponse = await deribit.Supporting.GetTime();
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
            HelloResponse helloresponse = await deribit.Supporting.Hello(new HelloRequest
            {
                client_name = "clientname",
                client_version = "1",
            });
            Assert.That(helloresponse.version, Is.Not.Null);
        }

        //----------------------------------------------------------------------------
    }
}
