using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.Trading
{
    public class BuySellResponse : ResponseBase
    {
        public Order order { get; set; }

        public IList<Trade> trades { get; set; }

        public bool rejected { get; set; }

        public string message { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BuySellResponseDto, BuySellResponse>()
                    .ForMember(d => d.rejected, o => o.Ignore())
                    .ForMember(d => d.message, o => o.Ignore())
                    ;
            }
        }

        public class Validator : FluentValidation.AbstractValidator<BuySellResponse>
        {
            public Validator()
            {
                this.RuleFor(x => x.order).SetValidator(new Order.Validator());
                this.RuleForEach(x => x.trades).SetValidator(new Trade.Validator());
                this.RuleFor(x => x.order).Null().When(x => x.rejected);
            }
        }
    }

    public class BuySellResponseDto
    {
        public OrderDto order { get; set; }

        public TradeDto[] trades { get; set; }
    }
}