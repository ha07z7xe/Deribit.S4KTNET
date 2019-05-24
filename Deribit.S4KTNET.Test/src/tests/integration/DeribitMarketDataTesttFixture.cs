using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.MarketData;
using FluentValidation;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class MarketDataTestFixture : DeribitIntegrationTestFixtureBase
    {

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [SetUp]
        public new async Task SetUp()
        {
            // dont spam
            await Task.Delay(1000);
        }

        //----------------------------------------------------------------------------
        // public/get_book_summary_by_instrument
        //----------------------------------------------------------------------------

        [Test]
        [Description("public/get_book_summary_by_instrument")]
        public async Task Test_getbooksummarybyinstrument_success()
        {
            // execute
            var books = await deribit.MarketData.GetBookSummaryByInstrument
                (new GetBookSummaryByInstrumentRequest()
            {
                instrument_name = DeribitInstruments.Perpetual.BTCPERPETUAL,
            });
            // assert
            Assert.That(books.Count, Is.GreaterThan(0));
            foreach (var book in books)
            {
                new BookSummary.Validator().ValidateAndThrow(book);
            }
        }

        //----------------------------------------------------------------------------
    }
}
