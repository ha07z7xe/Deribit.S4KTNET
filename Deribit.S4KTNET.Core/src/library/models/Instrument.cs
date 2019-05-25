using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class Instrument
    {
        public string InstrumentName { get; set; }

        public string QuoteCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public string Url { get; set; }

        public decimal TickSize { get; set; }

        public decimal ContractSize { get; set; }

        public decimal TakerFee { get; set; }

        public decimal MakerFee { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public DateTime ExpirationTimestamp { get; set; }

        public bool IsActive { get; set; }

        public InstrumentKind Kind { get; set; }

        public decimal MinTradeAmount { get; set; }

        public string OptionType { get; set; }

        public string SettlementPeriod { get; set; }

        public decimal? Strike { get; set; }
    }
}
