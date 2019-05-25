using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class OrderBook
    {
        public decimal? ask_iv { get; set; }

        public IList<BookOrder> asks { get; set; }

        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public decimal? bid_iv { get; set; }

        public IList<BookOrder> bids { get; set; }

        public decimal? current_funding { get; set; }

        public decimal? delivery_price { get; set; }

        public decimal? funding_8h { get; set; }

        public object greeks { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal last_price { get; set; }

        public decimal? mark_iv { get; set; }

        public decimal mark_price { get; set; }

        public decimal max_price { get; set; }

        public decimal min_price { get; set; }

        public decimal open_interest { get; set; }

        public string state { get; set; }

        public object stats { get; set; }

        public DateTime timestamp { get; set; }

        public decimal underlying_index { get; set; }

        public decimal underlying_price { get; set; }


        public class Validator : AbstractValidator<OrderBook>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.asks).NotEmpty();
                this.RuleForEach(x => x.asks).SetValidator(new BookOrder.Validator());
                this.RuleFor(x => x.bids).NotEmpty();
                this.RuleForEach(x => x.bids).SetValidator(new BookOrder.Validator());
                this.RuleFor(x => x.last_price).NotEmpty();
                this.RuleFor(x => x.index_price).NotEmpty();
                this.RuleFor(x => x.mark_price).NotEmpty();
                this.RuleFor(x => x.best_ask_amount).NotEmpty();
                this.RuleFor(x => x.best_ask_price).NotEmpty();
                this.RuleFor(x => x.best_bid_amount).NotEmpty();
                this.RuleFor(x => x.best_bid_price).NotEmpty();
                this.RuleFor(x => x.timestamp).NotEmpty();
            }
        }
    }
}
