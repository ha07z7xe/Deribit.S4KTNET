using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class Order
    {
        public string advanced { get; set; }

        public decimal amount { get; set; }

        public bool api { get; set; }

        public decimal average_price { get; set; }

        public decimal commission { get; set; }

        public DateTime creation_timestamp { get; set; }

        public BuySell direction { get; set; }

        public decimal filled_amount { get; set; }

        public decimal? implv { get; set; }

        public string instrument_name { get; set; }

        public bool is_liquidation { get; set; }

        public string label { get; set; }

        public DateTime last_update_timestamp { get; set; }

        public decimal max_show { get; set; }

        public string order_id { get; set; }

        public OrderState order_state { get; set; }

        public OrderType order_type { get; set; }

        public OrderType original_order_type { get; set; }

        public bool post_only { get; set; }

        public decimal? price { get; set; }

        public decimal profit_loss { get; set; }

        public bool reduce_only { get; set; }

        public decimal? stop_price { get; set; }

        public OrderTimeInForce time_in_force { get; set; }

        public OrderTriggerType? trigger { get; set; }

        public bool triggered { get; set; }

        public decimal? usd { get; set; }


        internal class Validator : FluentValidation.AbstractValidator<Order>
        {
            public Validator()
            {
                this.RuleFor(x => x.order_id).NotEmpty();
                this.RuleFor(x => x.amount).GreaterThan(0);
                this.RuleFor(x => x.filled_amount).GreaterThanOrEqualTo(0);
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }
}
