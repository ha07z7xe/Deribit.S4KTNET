using Deribit.S4KTNET.Core.Mapping;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.Trading
{
    public class ClosePositionResponse : ResponseBase
    {
        public Order order { get; set; }

        public IList<Trade> trades { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<ClosePositionResponseDto, ClosePositionResponse>()
                    ;
            }
        }

        public class Validator : FluentValidation.AbstractValidator<ClosePositionResponse>
        {
            public Validator()
            {
                this.RuleFor(x => x.order).SetValidator(new Order.Validator());
                this.RuleForEach(x => x.trades).SetValidator(new Trade.Validator());
            }
        }
    }

    internal class ClosePositionResponseDto
    {
        public OrderDto order { get; set; }

        public TradeDto[] trades { get; set; }
    }
}