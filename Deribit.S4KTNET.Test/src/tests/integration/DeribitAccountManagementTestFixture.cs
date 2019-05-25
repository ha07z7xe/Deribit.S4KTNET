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
                amount = 820,
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
                amount = 910,
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
