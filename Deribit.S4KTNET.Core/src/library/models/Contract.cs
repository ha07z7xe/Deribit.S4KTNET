using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class Contract
    {
        public string InstrumentName { get; set; }

        public string QuoteCurrency { get; set; }

        public string Url { get; set; }

        public decimal TickSize { get; set; }

        public decimal ContractSize { get; set; }

        public decimal TakerFee { get; set; }

        public decimal MakerFee { get; set; }
    }
}
