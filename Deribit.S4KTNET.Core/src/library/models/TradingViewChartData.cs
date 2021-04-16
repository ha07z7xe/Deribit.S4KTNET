using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class TradingViewChartData
    {
        public IList<decimal> volume { get; set; }
        public IList<decimal> cost { get; set; }
        public IList<DateTime> timestamps { get; set; }
        public string status { get; set; }
        public IList<decimal> open { get; set; }
        public IList<decimal> low { get; set; }
        public IList<decimal> high { get; set; }
        public IList<decimal> close { get; set; }


        public class Validator : AbstractValidator<TradingViewChartData>
        {
            public Validator()
            {
                //this.RuleFor(x => x.instrument_name).NotEmpty();
                //this.RuleFor(x => x.asks).NotEmpty();
                //this.RuleForEach(x => x.asks).SetValidator(new BookOrder.Validator());
                //this.RuleFor(x => x.bids).NotEmpty();
                //this.RuleForEach(x => x.bids).SetValidator(new BookOrder.Validator());
                //this.RuleFor(x => x.last_price).NotEmpty();
                //this.RuleFor(x => x.index_price).NotEmpty();
                //this.RuleFor(x => x.mark_price).NotEmpty();
                //this.RuleFor(x => x.best_ask_amount).NotEmpty();
                //this.RuleFor(x => x.best_ask_price).NotEmpty();
                //this.RuleFor(x => x.best_bid_amount).NotEmpty();
                //this.RuleFor(x => x.best_bid_price).NotEmpty();
                //this.RuleFor(x => x.timestamp).NotEmpty();
            }
        }
    }
}
