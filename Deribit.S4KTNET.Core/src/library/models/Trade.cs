using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class Trade
    {
        public decimal amount { get; set; }

        public BuySell direction { get; set; }

        public decimal fee { get; set; }

        public string fee_currency { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal iv { get; set; }

        public string label { get; set; }

        public string liquidity { get; set; }

        public string matching_id { get; set; }

        public string order_id { get; set; }

        public OrderType order_type { get; set; }

        public decimal price { get; set; }

        public bool self_trade { get; set; }

        public OrderState state { get; set; }

        public TickDirection tick_direction { get; set; }

        public DateTime timestamp { get; set; }

        public string trade_id { get; set; }

        public long trade_seq { get; set; }

        public class Validator : FluentValidation.AbstractValidator<Trade>
        {
            public Validator()
            {
                this.RuleFor(x => x.trade_id).NotEmpty();
                this.RuleFor(x => x.timestamp).NotEmpty();
                this.RuleFor(x => x.trade_seq).NotEmpty();
                this.RuleFor(x => x.price).NotEmpty();
                this.RuleFor(x => x.amount).GreaterThanOrEqualTo(0);
            }
        }
    }
}
