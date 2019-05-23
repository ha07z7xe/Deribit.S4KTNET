using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_margins

    public class GetMarginsRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public decimal amount { get; set; }

        public decimal price { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetMarginsRequest, GetMarginsRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetMarginsRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.amount).NotEmpty();
                this.RuleFor(x => x.price).NotEmpty();
            }
        }
    }

    public class GetMarginsRequestDto
    {
        public string instrument_name { get; set; }

        public decimal amount { get; set; }

        public decimal price { get; set; }
    }
}