using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.AccountManagement;
using NUnit.Framework;
using System.Threading.Tasks;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;
using System;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class DeribitAccountManagementTestFixture : DeribitIntegrationTestFixtureBase
    {

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

        //----------------------------------------------------------------------------
        // private/get_account_summary
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/get_account_summary")]
        public async Task Test_getaccountsummary_success()
        {
            // get account
            var account = await this.deribit.AccountManagement.GetAccountSummary(new GetAccountSummaryRequest
            {
                currency = CurrencyCode.BTC,
            });
            // assert
            new Account.Validator().ValidateAndThrow(account);
            Assert.That(account.currency, Is.EqualTo(CurrencyCode.BTC));
        }

        //----------------------------------------------------------------------------
        // private/get_position
        //----------------------------------------------------------------------------

        [Test, Order(1)]
        [Description("private/get_position")]
        public async Task Test_getposition_success()
        {
            // open position
            var buysellrequest = new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 30,
            };
            var order = (await this.deribit.Trading.Buy(buysellrequest)).order;
            // wait
            await Task.Delay(1 << 9);
            // get position
            var position = await this.deribit.AccountManagement.GetPosition(new GetPositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            });
            // assert
            new Position.Validator().ValidateAndThrow(position);
            Assert.That(position.size == order.amount);
            Assert.That(position.direction == order.direction);
            Assert.That(position.instrument_name == order.instrument_name);
            // wait
            await Task.Delay(1 << 9);
            // close position
            await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = "market",
            });
        }


        [Test, Order(1)]
        [Description("private/get_position (no position)")]
        public async Task Test_getposition_noposition()
        {
            //// open position
            //var buysellrequest = new BuySellRequest
            //{
            //    instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            //    type = OrderType.market,
            //    amount = 30,
            //};
            //var order = (await this.deribit.Trading.Buy(buysellrequest)).order;
            //// wait
            //await Task.Delay(1 << 9);
            // get position
            var position = await this.deribit.AccountManagement.GetPosition(new GetPositionRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            });
            // assert
            new Position.Validator().ValidateAndThrow(position);
            Assert.That(position.size == 0);
            Assert.That(position.direction == BuySell.Neutral);
            Assert.That(position.instrument_name == DeribitInstruments.Perpetual.BTCPERPETUAL);
        }

        //----------------------------------------------------------------------------
        // private/get_positions
        //----------------------------------------------------------------------------

        [Test, Order(1)]
        [Description("private/get_positions")]
        public async Task Test_getpositions_success()
        {
            // open positions
            await this.deribit.Trading.Sell(new BuySellRequest
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
                type = OrderType.market,
                amount = 20,
            });
            // wait
            await Task.Delay(1 << 9);
            // get position
            var positions = await this.deribit.AccountManagement.GetPositions(new GetPositionsRequest
            {
                currency = CurrencyCode.BTC,
            });
            // assert
            foreach (var p in positions)
            {
                new Position.Validator().ValidateAndThrow(p);
                Assert.That(p.size == 0);
                Assert.That(p.direction == BuySell.Neutral);
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


        [Test, Order(1)]
        [Description("private/get_positions (no position)")]
        public async Task Test_getpositions_noposition()
        {
            //// close positions
            //await this.deribit.Trading.ClosePosition(new ClosePositionRequest
            //{
            //    instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            //    type = "market",
            //});
            //// wait
            //await Task.Delay(1 << 9);
            // get positions
            var positions = await this.deribit.AccountManagement.GetPositions(new GetPositionsRequest
            {
                currency = CurrencyCode.BTC,
            });
            // assert
            foreach (var p in positions)
            {
                new Position.Validator().ValidateAndThrow(p);
            }
        }

        //----------------------------------------------------------------------------
        // private/get_subaccounts
        //----------------------------------------------------------------------------

        [Test, Order(1)]
        [Description("private/get_subaccounts")]
        [Ignore("Not Implemented")]
        public async Task Test_getsubaccounts_success()
        {
            //// get subaccounts
            //var subaccounts = await this.deribit.AccountManagement.GetSubAccounts(new GetSubAccountsRequest
            //{
            //    with_portfolio = true,
            //});
            //// assert
            //Assert.That(subaccounts.Count, Is.GreaterThan(0));
            //foreach (var a in subaccounts)
            //{
            //    new Account.Validator().ValidateAndThrow(a);
            //}
        }

        //----------------------------------------------------------------------------
    }
}
