using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-cancel_all_by_instrument

    public class CancelAllByInstrumentRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public string type { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<CancelAllByInstrumentRequest, CancelAllByInstrumentRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<CancelAllByInstrumentRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.type).Must(x => x == null || x == "all" || x == "limit" || x == "stop");
            }
        }
    }

    public class CancelAllByInstrumentRequestDto
    {
        public string instrument_name { get; set; }

        public string type { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<CancelAllByInstrumentRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.type).Must(x => x == null || x == "all" || x == "limit" || x == "stop");
            }
        }
    }
}