using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-close_position

    public class ClosePositionRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public string type { get; set; }

        public decimal? price { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<ClosePositionRequest, ClosePositionRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<ClosePositionRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.type).NotEmpty().Must(x => x == "limit" || x == "market");
                this.RuleFor(x => x.price).NotEmpty().When(x => x.type == "limit");
                this.RuleFor(x => x.price).Null().When(x => x.type != "limit");
            }
        }
    }

    public class ClosePositionRequestDto
    {
        public string instrument_name { get; set; }

        public string type { get; set; }

        public decimal? price { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<ClosePositionRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.type).NotEmpty().Must(x => x == "limit" || x == "market");
                this.RuleFor(x => x.price).NotEmpty().When(x => x.type == "limit");
                this.RuleFor(x => x.price).Null().When(x => x.type != "limit");
            }
        }
    }
}