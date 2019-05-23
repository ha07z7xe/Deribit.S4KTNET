using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.SubscriptionManagement;
using Deribit.S4KTNET.Core.Supporting;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class SubscriptionManagementTestFixture : DeribitIntegrationTestFixtureBase
    {
        private static readonly string[] channels = new string[]
        {
            DeribitSubscriptions.trades(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval.raw),
            DeribitSubscriptions.book(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval._100ms),
        };

        //----------------------------------------------------------------------------
        // public/subscription
        //----------------------------------------------------------------------------

        [Test, Order(1)]
        [Description("public/subscribe")]
        public async Task Test_subscribe()
        {
            SubscribeResponse subscriberesponse = await deribit.SubscriptionManagement
                .SubscribePublic(new SubscribeRequest()
            {
                channels = channels,
            });
            Assert.That(subscriberesponse.subscribed_channels.Length, Is.EqualTo(channels.Length));
        }

        [Test, Order(2)]
        [Description("public/unsubscribe")]
        public async Task Test_unsubscribe()
        {
            UnsubscribeResponse unsubscriberesponse = await deribit.SubscriptionManagement
                .UnsubscribePublic(new UnsubscribeRequest()
                {
                    channels = channels,
                });
            Assert.That(unsubscriberesponse.subscribed_channels.Length, Is.EqualTo(channels.Length));
        }

        //----------------------------------------------------------------------------
        // public/get_time
        //----------------------------------------------------------------------------


        //----------------------------------------------------------------------------
    }
}
