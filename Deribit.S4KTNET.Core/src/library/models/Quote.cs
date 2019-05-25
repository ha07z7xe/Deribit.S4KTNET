using FluentValidation;
using System;

namespace Deribit.S4KTNET.Core
{
    public class Quote
    {
        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public string instrument_name { get; set; }

        public DateTime timestamp { get; set; }


        public class Validator : AbstractValidator<Quote>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.best_ask_amount).GreaterThan(0);
                this.RuleFor(x => x.best_bid_amount).GreaterThan(0);
                this.RuleFor(x => x.best_ask_price).GreaterThan(x => x.best_bid_price);
                this.RuleFor(x => x.best_bid_price).GreaterThan(0);
                this.RuleFor(x => x.best_ask_price).GreaterThan(0);
            }
        }
    }
}
