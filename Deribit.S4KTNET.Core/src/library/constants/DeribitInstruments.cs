using System.Collections.Generic;

namespace Deribit.S4KTNET.Core
{
    public static class DeribitInstruments
    {
        public static class Future
        {
            // BTC-DMMMYY
            public const string BTC25MAR16 = "BTC-25MAR16";
            public const string BTC5AUG16 = "BTC-25MAR16";
            public const string BTC17MAY19 = "BTC-17MAY19";

            public const string ETH17MAY19 = "ETH-17MAY19";
            public const string ETH28JUN19 = "ETH-28JUN19";
        }

        public static class Perpetual
        {
            public const string BTCPERPETUAL = "BTC-PERPETUAL";
            public const string ETHPERPETUAL = "ETH-PERPETUAL";

        }

        public static class Option 
        {
            // BTC-DMMMYY-STRIKE-K 	
            public const string BTC25MAR16420C = "BTC-25MAR16-420-C";
        }

        public static readonly IDictionary<string, Instrument> ByInstrumentName
            = new Dictionary<string, Instrument>
            {
                [DeribitInstruments.Perpetual.BTCPERPETUAL] = new Instrument
                {
                    InstrumentName = DeribitInstruments.Perpetual.BTCPERPETUAL,
                    QuoteCurrency = "USD",
                    Url = "https://www.deribit.com/pages/docs/perpetual",
                    TickSize = 0.25m,
                    ContractSize = 10,
                    TakerFee = 0.00075m,
                    MakerFee = -0.00025m,
                },
                [DeribitInstruments.Perpetual.ETHPERPETUAL] = new Instrument
                {
                    InstrumentName = DeribitInstruments.Perpetual.ETHPERPETUAL,
                    QuoteCurrency = "USD",
                    Url = "https://www.deribit.com/pages/docs/perpetual",
                    TickSize = 0.01m,
                    ContractSize = 1,
                    TakerFee = 0.00075m,
                    MakerFee = -0.00025m,
                },
            };
    }
}