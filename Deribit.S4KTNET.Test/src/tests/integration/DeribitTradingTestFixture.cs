using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;
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
    class TradingTestFixture : DeribitIntegrationTestFixtureBase
    {
        //----------------------------------------------------------------------------
        // state
        //----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            if (!this.deribit.Authentication.IsAuthenticated)
            {
                throw new Exception("Authentication required for this test fixture");
            }
        }

        [SetUp]
        public new async Task SetUp()
        {
            // dont spam
            await Task.Delay(1000);
        }

        [TearDown]
        public new async Task TearDown()
        {
            // cancel all orders
        }

        //----------------------------------------------------------------------------
        // private/buy
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/buy (limit)")]
        public async Task Test_buy_limit_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETRUAL,
                amount = 10,
                type = OrderType.limit,
                label = "mylabel",
                price = 6000,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.buy(req);
            // assert
            new BuySellResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            Assert.That(response.order.order_id, Is.Not.Null);
            // cleanup
            await this.deribit.Trading.cancel(new CancelRequest()
            {
                order_id = response.order.order_id,
            });
        }

        [Test]
        [Description("private/buy (market)")]
        public async Task Test_buy_market_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETRUAL,
                amount = 1,
                type = OrderType.market,
                label = "mylabel",
                price = 1000,
                time_in_force = OrderTimeInForce.good_til_cancelled,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.buy(req);
            // assert
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            
        }

        [Test]
        [Description("private/buy (stoplimit)")]
        public async Task Test_buy_stoplimit_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETRUAL,
                amount = 1,
                type = OrderType.stop_limit,
                label = "mylabel",
                price = 1000,
                time_in_force = OrderTimeInForce.good_til_cancelled,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.buy(req);
            // assert
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
        }
        
        [Test]
        [Description("private/buy (stopmarket)")]
        public async Task Test_buy_stopmarket_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETRUAL,
                amount = 1,
                type = OrderType.stop_market,
                label = "mylabel",
                price = 1000,
                time_in_force = OrderTimeInForce.good_til_cancelled,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.buy(req);
            // assert
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
        }
        //----------------------------------------------------------------------------
        // private/sell
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/sell")]
        public async Task Test_sell_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {

            };
            // execute
            BuySellResponse response = await this.deribit.Trading.sell(req);
            // assert
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction == BuySell.Sell);
        }

        //----------------------------------------------------------------------------
        // private/cancel
        //----------------------------------------------------------------------------

        [Test]
        [Ignore("skipped")]
        public async Task Test_cancel()
        {
            
        }

        //----------------------------------------------------------------------------
        // private/cancel_all
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancelall()
        {
            // execute
            var response = await this.deribit.Trading.cancel_all();
            // assert
            Assert.That(response.success, Is.True);
        }

        //----------------------------------------------------------------------------
    }
}
