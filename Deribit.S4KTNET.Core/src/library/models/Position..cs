using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class Position
    {
        public decimal average_price { get; set; }

        public decimal? average_price_usd { get; set; }

        public decimal delta { get; set; }

        public BuySell direction { get; set; }

        public decimal? estimated_liquidation_price { get; set; }

        public decimal floating_profit_loss { get; set; }
        
        public decimal? floating_profit_loss_usd { get; set; }

        public decimal index_price { get; set; }

        public decimal initial_margin { get; set; }

        public string instrument_name { get; set; }

        public InstrumentKind kind { get; set; }

        public decimal maintenance_margin { get; set; }

        public decimal mark_price { get; set; }

        public decimal open_orders_margin { get; set; }

        public decimal realized_profit_loss { get; set; }

        public decimal settlement_price { get; set; }

        public decimal size { get; set; }

        public decimal? size_currency { get; set; }

        public decimal total_proft_loss { get; set; }


        public class Validator : FluentValidation.AbstractValidator<Position>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }
}
