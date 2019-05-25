using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_order_book

    public class GetOrderBookRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public int? depth { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetOrderBookRequest, GetOrderBookRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetOrderBookRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.depth).GreaterThan(0).When(x => x.depth.HasValue);
            }
        }
    }

    public class GetOrderBookRequestDto
    {
        public string instrument_name { get; set; }

        public int? depth { get; set; }
    }
}