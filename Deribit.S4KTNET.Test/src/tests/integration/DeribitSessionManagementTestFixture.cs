using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.SessionManagement;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using rx = System.Reactive;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class SessionManagementTestFixture : DeribitIntegrationTestFixtureBase, IObserver<rx.Unit>
    {
        //----------------------------------------------------------------------------
        // state
        //----------------------------------------------------------------------------

        private int heartbeatnotificationcount = 0;

        //----------------------------------------------------------------------------
        // public/set_heartbeat
        //----------------------------------------------------------------------------

        [Test, Order(1)]
        [Description("public/set_heartbeat")]
        public async Task Test_setheartbeat_success()
        {
            using (var sub = this.deribit.SessionManagement.HeartbeatStream.Subscribe(this))
            {
                SetHeartbeatResponse setheartbeatresponse = await deribit.SessionManagement.SetHeartbeat(new SetHeartbeatRequest()
                {
                    interval = 10,
                });
                Assert.That(setheartbeatresponse.result, Is.True);
                await Task.Delay(TimeSpan.FromSeconds(15));
            }
            Assert.That(heartbeatnotificationcount, Is.GreaterThan(0));
        }

        //----------------------------------------------------------------------------
        // public/disable_heartbeat
        //----------------------------------------------------------------------------

        [Test, Order(2)]
        [Description("public/disable_heartbeat")]
        public async Task Test_disableheartbeat_success()
        {
            DisableHeartbeatResponse disableheartbeatresponse = await deribit.SessionManagement.DisableHeartbeat();
            Assert.That(disableheartbeatresponse.result, Is.True);
        }

        //----------------------------------------------------------------------------
        // private/enable_cancel_on_disconnect
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/enable_cancel_on_disconnect")]
        public async Task Test_enable_cancel_on_disconnect_success()
        {
            if (!this.deribit.Authentication.IsAuthenticated)
                throw new Exception("Test requires authentication");
            EnableCancelOnDisconnectResponse response = await deribit.SessionManagement.EnableCancelOnDisconnect();
            Assert.That(response.result, Is.True);
        }

        //----------------------------------------------------------------------------
        // private/disable_cancel_on_disconnect
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/disable_cancel_on_disconnect")]
        public async Task Test_disable_cancel_on_disconnect_success()
        {
            if (!this.deribit.Authentication.IsAuthenticated)
                throw new Exception("Test requires authentication");
            DisableCancelOnDisconnectResponse response = await deribit.SessionManagement.DisableCancelOnDisconnect();
            Assert.That(response.result, Is.True);
        }

        //----------------------------------------------------------------------------
        // heartbeat notifications
        //----------------------------------------------------------------------------

        public void OnNext(rx.Unit value)
        {
            Interlocked.Increment(ref this.heartbeatnotificationcount);
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        //----------------------------------------------------------------------------
    }
}
