using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class BookSummary
    {
        public decimal ask_price { get; set; }

        public string base_currency { get; set; }

        public decimal bid_price { get; set; }

        public DateTime creation_timestamp { get; set; }

        public decimal? current_funding { get; set; }

        public decimal estimated_delivery_price { get; set; }

        public decimal? funding_8h { get; set; }

        public decimal high { get; set; }

        public string instrument_name { get; set; }

        public decimal? interest_rate { get; set; }

        public decimal last { get; set; }

        public decimal low { get; set; }

        public decimal mark_price { get; set; }

        public decimal mid_price { get; set; }

        public decimal open_interest { get; set; }

        public string quote_currency { get; set; }

        public string underlying_index { get; set; }

        public decimal? underlying_price { get; set; }

        public decimal volume { get; set; }

        public decimal? volume_usd { get; set; }


        public class Validator : FluentValidation.AbstractValidator<BookSummary>
        {
            public Validator()
            {
                this.RuleFor(x => x.high).GreaterThanOrEqualTo(x => x.low);
                this.RuleFor(x => x.ask_price).NotEmpty();
                this.RuleFor(x => x.bid_price).NotEmpty();
                this.RuleFor(x => x.ask_price).GreaterThan(x => x.bid_price);
                this.RuleFor(x => x.volume).NotEmpty();
                this.RuleFor(x => x.last).NotEmpty();
                this.RuleFor(x => x.creation_timestamp).NotEmpty();
            }
        }
    }
}
