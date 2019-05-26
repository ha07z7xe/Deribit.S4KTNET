using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-ticker

    public class TickerRequest : RequestBase
    {
        public string instrument_name { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TickerRequest, TickerRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TickerRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }

    internal class TickerRequestDto
    {
        public string instrument_name { get; set; }
    }
}