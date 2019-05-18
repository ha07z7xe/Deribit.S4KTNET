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
    class NotificationsTestFixture : DeribitIntegrationTestFixtureBase,
        IObserver<AnnouncementsNotification>,
        IObserver<BookDepthLimitedNotification>,
        IObserver<BookFullNotification>,
        IObserver<DeribitPriceIndexNotification>,
        IObserver<QuoteNotification>
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
            //DeribitSubscriptions.announcements,
            //DeribitSubscriptions.book(DeribitInstruments.Perpetual.BTCPERPETRUAL, OrderbookGrouping._5, OrderbookDepth._10, Interval._100ms),
            DeribitSubscriptions.book(DeribitInstruments.Perpetual.BTCPERPETRUAL, Interval._100ms),
            DeribitSubscriptions.deribit_price_index(DeribitIndices.btc_usd),
            DeribitSubscriptions.quote(DeribitInstruments.Perpetual.BTCPERPETRUAL),
        };

        //----------------------------------------------------------------------------
        // state
        //----------------------------------------------------------------------------

        private int announcementnotificationcount = 0;
        private int bookdepthlimitednotificationcount = 0;
        private int bookfullnotificationcount = 0;
        private int deribitpriceindexnotificationcount = 0;
        private int quotenotificationcount = 0;

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            SubscribeResponse subscriberesponse = await deribit.SubscriptionManagement
                .subscribe_public(new SubscribeRequest()
            {
                channels = channels,
            });
            Assert.That(subscriberesponse.subscribed_channels.Length, Is.EqualTo(channels.Length));
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            UnsubscribeResponse unsubscriberesponse = await deribit.SubscriptionManagement
                .unsubscribe_public(new UnsubscribeRequest()
                {
                    channels = channels,
                });
            Assert.That(unsubscriberesponse.subscribed_channels.Length, Is.EqualTo(channels.Length));
        }

        //----------------------------------------------------------------------------
        // announcements
        //----------------------------------------------------------------------------

        [Test]
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
            Interlocked.Increment(ref this.announcementnotificationcount);
        }

        //----------------------------------------------------------------------------
        // book depth limited
        //----------------------------------------------------------------------------

        [Test]
        [Ignore("unauthorized")]
        public async Task BookDepthLimitedStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.book)));
            using (var sub = this.deribit.SubscriptionManagement.BookDepthLimitedStream.Subscribe(this))
            {
                await Task.Delay(mediumwait);
            }
            //Assert.That(this.bookdepthlimitednotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(BookDepthLimitedNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(BookDepthLimitedNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.bookdepthlimitednotificationcount);
        }

        //----------------------------------------------------------------------------
        // book full
        //----------------------------------------------------------------------------

        [Test]
        public async Task BookFullStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.book)));
            using (var sub = this.deribit.SubscriptionManagement.BookFullStream.Subscribe(this))
            {
                await Task.Delay(mediumwait);
            }
            Assert.That(this.bookfullnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(BookFullNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(BookFullNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.bookfullnotificationcount);
        }

        //----------------------------------------------------------------------------
        // deribit price index
        //----------------------------------------------------------------------------

        [Test]
        public async Task DeribitPriceIndexStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.deribit_price_index)));
            using (var sub = this.deribit.SubscriptionManagement.DeribitPriceIndexStream.Subscribe(this))
            {
                await Task.Delay(mediumwait);
            }
            Assert.That(this.deribitpriceindexnotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(DeribitPriceIndexNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(DeribitPriceIndexNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.deribitpriceindexnotificationcount);
        }

        //----------------------------------------------------------------------------
        // quote
        //----------------------------------------------------------------------------

        [Test]
        public async Task QuoteStream()
        {
            Assert.That(channels.Any(c => c.StartsWith(DeribitChannelPrefix.quote)));
            using (var sub = this.deribit.SubscriptionManagement.QuoteStream.Subscribe(this))
            {
                await Task.Delay(mediumwait);
            }
            Assert.That(this.quotenotificationcount, Is.GreaterThan(0));
        }

        public void OnNext(QuoteNotification value)
        {
            Log.Information($"{value.channel} | Received {nameof(QuoteNotification)} {value.sequencenumber}");
            Interlocked.Increment(ref this.quotenotificationcount);
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
