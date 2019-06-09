using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class DeribitTradingTestFixture : DeribitIntegrationTestFixtureBase
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
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
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
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
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

        [Test]
        [Description("private/buy (reduceonly)")]
        public async Task Test_buy_reduceonly()
        {
            // close existing position
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
            // wait
            await Task.Delay(1 << 9);
            // open long position
            BuySellResponse buysellresponse = await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 20,
                type = OrderType.market,
            });
            Assert.That(buysellresponse.order, Is.Not.Null);
            // wait
            await Task.Delay(1 << 9);
            // try increase position
            buysellresponse = await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 20,
                type = OrderType.market,
                reduce_only = true,
            });
            // assert
            Assert.That(buysellresponse.rejected, Is.True);
            Assert.That(buysellresponse.message.Contains("reduce_only"));
            // wait
            await Task.Delay(1 << 9);
            // try reduce position
            buysellresponse = await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.market,
                reduce_only = true,
            });
            // assert
            Assert.That(buysellresponse.order, Is.Not.Null);
            Assert.That(buysellresponse.rejected, Is.False);
            // wait
            await Task.Delay(1 << 9);
            // cleanup
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
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
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
        }

        [Test]
        [Description("private/sell (reduceonly)")]
        public async Task Test_sell_reduceonly()
        {
            // close existing position
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
            // wait
            await Task.Delay(1 << 9);
            // open short position
            BuySellResponse sellsellresponse = await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 20,
                type = OrderType.market,
            });
            Assert.That(sellsellresponse.order, Is.Not.Null);
            // wait
            await Task.Delay(1 << 9);
            // try increase position
            sellsellresponse = await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 20,
                type = OrderType.market,
                reduce_only = true,
            });
            // assert
            Assert.That(sellsellresponse.rejected, Is.True);
            Assert.That(sellsellresponse.message.Contains("reduce_only"));
            // wait
            await Task.Delay(1 << 9);
            // try reduce position
            sellsellresponse = await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 10,
                type = OrderType.market,
                reduce_only = true,
            });
            // assert
            Assert.That(sellsellresponse.order, Is.Not.Null);
            Assert.That(sellsellresponse.rejected, Is.False);
            // wait
            await Task.Delay(1 << 9);
            // cleanup
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
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
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
        }

        //----------------------------------------------------------------------------
        // private/cancel
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancel()
        {
            //----------------------------------------------------------------------------
            // submit bid
            //----------------------------------------------------------------------------
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 30,
                type = OrderType.limit,
                label = "mylabel",
                price = 1100,
                post_only = true,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            Assert.That(response.order, Is.Not.Null);
            // wait 
            await Task.Delay(1 << 9);
            //----------------------------------------------------------------------------
            // cancel bid
            //----------------------------------------------------------------------------
            // form request
            CancelRequest req2 = new CancelRequest()
            {
                order_id = response.order.order_id,
            };
            // execute request
            Order cancelledorder = await this.deribit.Trading.Cancel(req2);
            // assert
            Assert.That(cancelledorder.order_id, Is.EqualTo(response.order.order_id));
            Assert.That(cancelledorder.order_state, Is.EqualTo(OrderState.cancelled));
            //----------------------------------------------------------------------------
        }

        //----------------------------------------------------------------------------
        // private/cancel_all
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancelall()
        {
            //----------------------------------------------------------------------------
            // submit bid
            //----------------------------------------------------------------------------
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 20,
                type = OrderType.limit,
                label = "mylabel",
                price = 1520,
                post_only = true,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            Assert.That(response.order, Is.Not.Null);
            //----------------------------------------------------------------------------
            // cancel all
            //----------------------------------------------------------------------------
            // wait 
            await Task.Delay(1 << 9);
            // execute
            var response2 = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
            //----------------------------------------------------------------------------
            // cancel all again
            //----------------------------------------------------------------------------
            // wait 
            await Task.Delay(1 << 9);
            // execute
            var response3 = await this.deribit.Trading.CancelAll();
            // assert
            Assert.That(response3.cancelledcount, Is.EqualTo(0));
            //----------------------------------------------------------------------------
        }

        //----------------------------------------------------------------------------
        // private/cancel_all_by_currency
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancelallbycurrency()
        {
            //----------------------------------------------------------------------------
            // submit bid
            //----------------------------------------------------------------------------
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 30,
                type = OrderType.limit,
                label = "mylabel",
                price = 1010,
                post_only = true,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            Assert.That(response.order, Is.Not.Null);
            //----------------------------------------------------------------------------
            // cancel all
            //----------------------------------------------------------------------------
            // wait 
            await Task.Delay(1 << 9);
            // execute
            var response2 = await this.deribit.Trading.CancelAllByCurrency(new CancelAllByCurrencyRequest
            {
                currency = CurrencyCode.BTC,
            });
            // assert
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
        }

        //----------------------------------------------------------------------------
        // private/cancel_all_by_instrument
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_cancelallbyinstrument()
        {
            //----------------------------------------------------------------------------
            // submit bid
            //----------------------------------------------------------------------------
            // form request
            BuySellRequest req = new BuySellRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                amount = 30,
                type = OrderType.limit,
                label = "mylabel",
                price = 930,
                post_only = true,
            };
            // execute
            BuySellResponse response = await this.deribit.Trading.Buy(req);
            Assert.That(response.order, Is.Not.Null);
            //----------------------------------------------------------------------------
            // cancel all
            //----------------------------------------------------------------------------
            // wait 
            await Task.Delay(1 << 9);
            // execute
            var response2 = await this.deribit.Trading.CancelAllByInstrument(new CancelAllByInstrumentRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            });
            // assert
            Assert.That(response2.cancelledcount, Is.GreaterThan(0));
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
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 10,
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
            Assert.That(response.already_closed, Is.False);
        }

        [Test]
        public async Task Test_closeposition_noexistingposition()
        {
            // close position
            var response = await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
            new ClosePositionResponse.Validator().ValidateAndThrow(response);
            // wait
            await Task.Delay(1 << 9);
            // close position
            response = await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
            // assert
            new ClosePositionResponse.Validator().ValidateAndThrow(response);
            Assert.That(response.already_closed, Is.True);
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
        // private/get_open_orders_by_currency
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getopenordersbycurrency()
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
                amount = 10,
                price = 30000,
            });
            // wait
            await Task.Delay(1 << 9);
            // form request
            var request = new GetOpenOrdersByCurrencyRequest()
            {
                currency = CurrencyCode.BTC,
            };
            // execute
            var response = await this.deribit.Trading.GetOpenOrdersByCurrency(request);
            // assert
            Assert.That(response.Count, Is.EqualTo(2));
            // wait
            await Task.Delay(1 << 9);
            // cleanup
            await this.deribit.Trading.CancelAll();
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
                amount = 30,
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
        // private/get_order_history_by_currency
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getorderhistorybycurrency()
        {
            // cleanup
            await this.deribit.Trading.CancelAll();
            // wait
            await Task.Delay(1 << 9);
            // submit orders
            await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 20,
            });
            await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 20,
            });
            // wait
            await Task.Delay(1 << 9);
            // form request
            var request = new GetOrderHistoryByCurrencyRequest()
            {
                currency = CurrencyCode.BTC,
                count = 2,
            };
            // execute
            var response = await this.deribit.Trading.GetOrderHistoryByCurrency(request);
            // assert
            Assert.That(response.Count, Is.EqualTo(2));
            // wait
            await Task.Delay(1 << 9);
            // cleanup
            await this.deribit.Trading.CancelAll();
        }

        //----------------------------------------------------------------------------
        // private/get_order_history_by_instrument
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getorderhistorybyinstrument()
        {
            // cleanup
            await this.deribit.Trading.CancelAll();
            // wait
            await Task.Delay(1 << 9);
            // submit orders
            await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 20,
            });
            await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 20,
            });
            // wait
            await Task.Delay(1 << 9);
            // form request
            var request = new GetOrderHistoryByInstrumentRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                count = 2,
            };
            // execute
            var response = await this.deribit.Trading.GetOrderHistoryByInstrument(request);
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
        // private/get_user_trades_by_instrument
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getusertradesbyinstrument()
        {
            // make some trades
            await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 20,
            });
            // wait
            await Task.Delay(1 << 9);
            // get trades
            var response = await this.deribit.Trading.GetUserTradesByInstrument(new GetUserTradesByInstrumentRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            });
            var trades = response.trades;
            // assert
            Assert.That(trades.Count, Is.GreaterThan(0));
            // wait
            await Task.Delay(1 << 9);
            // close position
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
        }

        //----------------------------------------------------------------------------
        // private/get_user_trades_by_order
        //----------------------------------------------------------------------------

        [Test]
        public async Task Test_getusertradesbyorder()
        {
            // make some trades
            var buysellresponse = await this.deribit.Trading.Buy(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 10,
            });
            var order = buysellresponse.order;
            // wait
            await Task.Delay(1 << 9);
            // get trades
            var trades = await this.deribit.Trading.GetUserTradesByOrder(new GetUserTradesByOrderRequest
            {
                order_id = order.order_id,
            });
            // assert
            Assert.That(trades.Count, Is.GreaterThan(0));
            foreach (var trade in trades)
            {
                new Trade.Validator().ValidateAndThrow(trade);
                Assert.That(trade.order_id, Is.EqualTo(order.order_id));
            }
            // wait
            await Task.Delay(1 << 9);
            // close position
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
        }

        //----------------------------------------------------------------------------
    }
}
