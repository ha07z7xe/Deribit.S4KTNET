using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.SessionManagement;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class SessionManagementTestFixture : DeribitIntegrationTestFixtureBase, IObserver<Unit>
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
                await Task.Delay(TimeSpan.FromSeconds(24));
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
        // heartbeat notifications
        //----------------------------------------------------------------------------

        public void OnNext(Unit value)
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
