using System.Collections.Generic;

namespace Deribit.S4KTNET.Core
{
    public static class DeribitContracts
    {
        public static readonly IDictionary<string, Contract> ByInstrumentName
            = new Dictionary<string, Contract>
            {
                [DeribitInstruments.Perpetual.BTCPERPETRUAL] = new Contract
                {
                    InstrumentName = DeribitInstruments.Perpetual.BTCPERPETRUAL,
                    QuoteCurrency = "USD",
                    Url = "https://www.deribit.com/pages/docs/perpetual",
                    TickSize = 0.25m,
                    ContractSize = 10,
                    TakerFee = 0.00075m,
                    MakerFee = -0.00025m,
                },
                [DeribitInstruments.Perpetual.ETHPERPETRUAL] = new Contract
                {
                    InstrumentName = DeribitInstruments.Perpetual.ETHPERPETRUAL,
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