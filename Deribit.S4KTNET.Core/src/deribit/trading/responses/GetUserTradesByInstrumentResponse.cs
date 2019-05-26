using Deribit.S4KTNET.Core.Mapping;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.Trading
{
    public class GetUserTradesByInstrumentResponse : ResponseBase
    {
        public bool has_more { get; set; }

        public IList<Trade> trades { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetUserTradesByInstrumentResponseDto, GetUserTradesByInstrumentResponse>()
                    ;
            }
        }

        public class Validator : FluentValidation.AbstractValidator<GetUserTradesByInstrumentResponse>
        {
            public Validator()
            {
                this.RuleForEach(x => x.trades).SetValidator(new Trade.Validator());
            }
        }
    }

    internal class GetUserTradesByInstrumentResponseDto
    {
        public bool has_more { get; set; }

        public TradeDto[] trades { get; set; }
    }
}