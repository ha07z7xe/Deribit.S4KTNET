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

        public new async Task OneTimeTearDown()
        {
            await this.deribit.Trading.CancelAll();
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
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.limit,
                label = "mylabel",
                price = 2000,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            // assert
            new BuySellResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            Assert.That(response.order.order_id, Is.Not.Null);
            // wait 
            await Task.Delay(1 << 9);
            // cleanup
            var response2 = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response2.success, Is.True);
        }

        [Test]
        [Description("private/buy (market)")]
        public async Task Test_buy_market_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.market,
                label = "mylabel",
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            // assert
            new BuySellResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            Assert.That(response.order.order_id, Is.Not.Null);
            Assert.That(response.trades.Count, Is.GreaterThan(0));
            // wait 
            await Task.Delay(1 << 9);
            // cleanup
            var response2 = await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
            // assert
            new ClosePositionResponse.Validator().ValidateAndThrow(response2);
        }

        [Test]
        [Description("private/buy (stoplimit)")]
        public async Task Test_buy_stoplimit_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.stop_limit,
                label = "mylabel",
                price = 11000,
                stop_price = 10000,
                trigger = OrderTriggerType.index_price,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            // assert
            new BuySellResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            Assert.That(response.order.order_id, Is.Not.Null);
            // wait 
            await Task.Delay(1 << 9);
            // cleanup
            var response2 = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response2.success, Is.True);
        }
        
        [Test]
        [Description("private/buy (stopmarket)")]
        public async Task Test_buy_stopmarket_success()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.stop_market,
                label = "mylabel",
                stop_price = 10000,
                trigger = OrderTriggerType.index_price,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            // assert
            new BuySellResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Buy));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            Assert.That(response.order.order_id, Is.Not.Null);
        }

        //----------------------------------------------------------------------------
        // private/sell
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/sell")]
        public async Task Test_sell_postonly()
        {
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.limit,
                label = "mylabel",
                price = 1000,
                post_only = true,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Sell(req);
            // assert
            new BuySellResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.order, Is.Not.Null);
            Assert.That(response.order.direction, Is.EqualTo(BuySell.Sell));
            Assert.That(response.order.label, Is.EqualTo(req.label));
            Assert.That(response.order.order_id, Is.Not.Null);
            Assert.That(response.trades.Count, Is.EqualTo(0));
            // wait 
            await Task.Delay(1 << 9);
            // cleanup
            var response2 = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response2.success, Is.True);
        }

        //----------------------------------------------------------------------------
        // private/edit
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/edit")]
        public async Task Test_editorder()
        {
            //----------------------------------------------------------------------------
            // submit bid
            //----------------------------------------------------------------------------
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.limit,
                label = "mylabel",
                price = 1000,
                post_only = true,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            Assert.That(response.order, Is.Not.Null);
            // wait 
            await Task.Delay(1 << 9);
            //----------------------------------------------------------------------------
            // modify bid
            //----------------------------------------------------------------------------
            // form request
            EditOrderRequest req2 = new EditOrderRequest()
            {
                order_id = response.order.order_id,
                amount = 20,
                price = 500,
            };
            // execute request
            EditOrderResponse res2 = await this.deribit.Trading.EditOrder(req2);
            // assert
            var modifiedorder = res2.order;
            Assert.That(modifiedorder.order_id, Is.EqualTo(req2.order_id));
            Assert.That(modifiedorder.amount, Is.EqualTo(req2.amount));
            Assert.That(modifiedorder.price, Is.EqualTo(req2.price));
            //----------------------------------------------------------------------------
            // cleanup
            var response2 = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response2.success, Is.True);
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
            var response = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response.success, Is.True);
        }

        //----------------------------------------------------------------------------
        // private/cancel_all_by_currency
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancelallbycurrency()
        {
            // execute
            var response = await this.deribit.Trading.CancelAllByCurrency(new CancelAllByCurrencyRequest
            {
                currency = Currency.BTC,
            });
            // assert
            Assert.That(response.success, Is.True);
        }

        //----------------------------------------------------------------------------
        // private/cancel_all_by_instrument
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancelallbyinstrument()
        {
            // execute
            var response = await this.deribit.Trading.CancelAllByInstrument(new CancelAllByInstrumentRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            });
            // assert
            Assert.That(response.success, Is.True);
        }

        //----------------------------------------------------------------------------
        // private/close_position
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_closeposition()
        {
            // open a position
            await this.deribit.Trading.Buy(new BuySellRequest
            {
                amount = 10,
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
            });
            // wait
            await Task.Delay(1 << 9);
            // close position
            var response = await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
            // assert
            new ClosePositionResponse.Validator().ValidateAndThrow(response);
        }

        //----------------------------------------------------------------------------
        // private/get_margins
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getmargins()
        {
            // form request
            var req = new GetMarginsRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 20,
                price = 5000,
            };
            // execute request
            var response = await this.deribit.Trading.GetMargins(req);
            // assert
            new GetMarginsResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.buy, Is.GreaterThan(0));
        }

        //----------------------------------------------------------------------------
        // private/get_open_orders_by_instrument
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getopenordersbyinstrument()
        {
            // cleanup
            await this.deribit.Trading.CancelAll();
            // wait
            await Task.Delay(1 << 9);
            // submit orders
            await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.limit,
                amount = 20,
                price = 500,
            });
            await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.limit,
                amount = 50,
                price = 30000,
            });
            // wait
            await Task.Delay(1 << 9);
            // form request
            var request = new GetOpenOrdersByInstrumentRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            };
            // execute
            var response = await this.deribit.Trading.GetOpenOrdersByInstrument(request);
            // assert
            Assert.That(response.Count, Is.EqualTo(2));
            // wait
            await Task.Delay(1 << 9);
            // cleanup
            await this.deribit.Trading.CancelAll();
        }


        //----------------------------------------------------------------------------
        // private/get_order_state
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getorderstate()
        {
            // submit orders
            var buysellresponse = await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.limit,
                amount = 20,
                price = 560,
            });
            var order = buysellresponse.order;
            // wait
            await Task.Delay(1 << 9);
            // execute
            var orderstate = await this.deribit.Trading.GetOrderState(new GetOrderStateRequest()
            {
                order_id = order.order_id,
            });
            // assert
            Assert.That(orderstate.order_id, Is.EqualTo(order.order_id));
            Assert.That(orderstate.order_type, Is.EqualTo(order.order_type));
            Assert.That(orderstate.price, Is.EqualTo(order.price));
            // wait
            await Task.Delay(1 << 9);
            // cleanup
            await this.deribit.Trading.CancelAll();
        }

        //----------------------------------------------------------------------------
    }
}
