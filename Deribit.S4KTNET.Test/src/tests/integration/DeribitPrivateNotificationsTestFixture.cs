using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.SubscriptionManagement;
using NUnit.Framework;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    [Parallelizable(ParallelScope.Children)]
    class DeribitPrivateNotificationsTestFixture : DeribitIntegrationTestFixtureBase,
        IObserver<AnnouncementsNotification>,
        IObserver<UserOrdersNotification>
    {
        //----------------------------------------------------------------------------
        // configuration
        //----------------------------------------------------------------------------

        private static readonly TimeSpan shortwait = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan mediumwait = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan longwait = TimeSpan.FromSeconds(20);

        //----------------------------------------------------------------------------
        // fields
        //----------------------------------------------------------------------------

        private static readonly string[] channels = new string[]
        {
            DeribitSubscriptions.user.orders(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval._100ms),
        };

        //----------------------------------------------------------------------------
        // state
        //----------------------------------------------------------------------------

        private int announcementsnotificationcount = 0;
        private int userordersnotificationcount = 0;

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public new async Task OneTimeSetUp()
        {
            SubscribeResponse subscriberesponse = await deribit.SubscriptionManagement
                .SubscribePrivate(new SubscribeRequest()
            {
                channels = channels,
            });
            Assert.That(subscriberesponse.subscribed_channels.Length, Is.EqualTo(channels.Length));
        }

        [OneTimeTearDown]
        public new async Task OneTimeTearDown()
        {
            UnsubscribeResponse unsubscriberesponse = await deribit.SubscriptionManagement
                .UnsubscribePrivate(new UnsubscribeRequest()
                {
                    channels = channels,
                });
            Assert.That(unsubscriberesponse.subscribed_channels.Length, Is.EqualTo(channels.Length));
        }

        //[TearDown]
        //public new void TearDown()
        //{
        //    this.announcementsnotificationcount = 0;
        //    this.userordersnotificationcount = 0;
        //}

        //----------------------------------------------------------------------------
        // announcements
        //----------------------------------------------------------------------------

        [Test]
        [Retry(3)]
        [Ignore("unauthorized")]
        public async Task TestAnnouncementsStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.announcements)));
            using (var sub = this.deribit.SubscriptionManagement.AnnouncementsStream.Subscribe(this))
            {
                await Task.Delay(shortwait);
            }
            //Assert.That(this.announcementnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(AnnouncementsNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(AnnouncementsNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.announcementsnotificationcount);
        }

        //----------------------------------------------------------------------------
        // user.orders
        //----------------------------------------------------------------------------

        [Test]
        public async Task UserOrdersStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.userorders)));
            Assert.That(deribit.Authentication.IsAuthenticated);
            using (var sub = this.deribit.SubscriptionManagement.UserOrdersStream.Subscribe(this))
            {
                // make an order
                await this.deribit.Trading.Buy(new Core.Trading.BuySellRequest()
                {
                    instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                    amount = 10,
                    type = OrderType.limit,
                    label = "mylabel",
                    price = 2000,
                });
            }
            Assert.That(this.userordersnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(UserOrdersNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(UserOrdersNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.userordersnotificationcount);
        }

        //----------------------------------------------------------------------------

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
