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
        IObserver<UserOrdersNotification>,
        IObserver<UserTradesNotification>,
        IObserver<UserPortfolioNotification>
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
            DeribitSubscriptions.user.orders(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval.raw),
            DeribitSubscriptions.user.trades(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval.raw),
            DeribitSubscriptions.user.portfolio(CurrencyCode.BTC),
        };

        //----------------------------------------------------------------------------
        // state
        //----------------------------------------------------------------------------

        private int announcementsnotificationcount = 0;
        private int userordersnotificationcount = 0;
        private int userportfoliosnotificationcount = 0;
        private int usertradesnotificationcount = 0;

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public new async Task OneTimeSetUp()
        {
            Assert.That(this.deribit.Authentication.IsAuthenticated);
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
            using (var sub = this.deribit.SubscriptionManagement.UserOrdersStream.Subscribe(this))
            {
                // make an order
                var buysellresponse = await this.deribit.Trading.Buy(new Core.Trading.BuySellRequest()
                {
                    instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                    amount = 10,
                    type = OrderType.limit,
                    label = "mylabel",
                    price = 2000,
                });
                // wait
                await Task.Delay(shortwait);
                // cancel order
                await this.deribit.Trading.Cancel(new Core.Trading.CancelRequest
                {
                    order_id = buysellresponse.order.order_id,
                });
                // wait
                await Task.Delay(shortwait);
            }
            Assert.That(this.userordersnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(UserOrdersNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(UserOrdersNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.userordersnotificationcount);
        }

        //----------------------------------------------------------------------------
        // user.portfolio
        //----------------------------------------------------------------------------

        [Test]
        public async Task UserPortfolioStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.userportfolio)));
            using (var sub = this.deribit.SubscriptionManagement.UserPortfolioStream.Subscribe(this))
            {
                // make an order
                await this.deribit.Trading.Buy(new Core.Trading.BuySellRequest()
                {
                    instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                    amount = 210,
                    type = OrderType.market,
                    label = "mylabel",
                });
                // wait
                await Task.Delay(shortwait);
            }
            Assert.That(this.userportfoliosnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(UserPortfolioNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(UserPortfolioNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.userportfoliosnotificationcount);
        }

        //----------------------------------------------------------------------------
        // user.trades
        //----------------------------------------------------------------------------

        [Test]
        public async Task UserTradesStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.usertrades)));
            using (var sub = this.deribit.SubscriptionManagement.UserTradesStream.Subscribe(this))
            {
                // make an trade
                await this.deribit.Trading.Buy(new Core.Trading.BuySellRequest()
                {
                    instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                    amount = 930,
                    type = OrderType.market,
                    label = "mylabel",
                });
                // wait
                await Task.Delay(shortwait);
            }
            Assert.That(this.usertradesnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(UserTradesNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(UserTradesNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.usertradesnotificationcount);
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
