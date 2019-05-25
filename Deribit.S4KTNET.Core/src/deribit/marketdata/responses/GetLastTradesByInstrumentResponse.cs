using Deribit.S4KTNET.Core.Mapping;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_last_trades_by_instrument

    public class GetLastTradesByInstrumentResponse : ResponseBase
    {
        public bool has_more { get; set; }

        public IList<Trade> trades { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetLastTradesByInstrumentResponseDto, GetLastTradesByInstrumentResponse>();
            }
        }

        public class Validator : FluentValidation.AbstractValidator<GetLastTradesByInstrumentResponse>
        {
            public Validator()
            {
                RuleFor(x => x.trades).NotNull();
                RuleFor(x => x.trades).NotEmpty().When(x => x.has_more);
                RuleForEach(x => x.trades).SetValidator(new Trade.Validator());
            }
        }
    }

    public class GetLastTradesByInstrumentResponseDto
    {
        public bool has_more { get; set; }

        public TradeDto[] trades { get; set; }
    }
}