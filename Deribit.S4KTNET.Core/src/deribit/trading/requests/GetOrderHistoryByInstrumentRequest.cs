using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_order_history_by_instrument

    public class GetOrderHistoryByInstrumentRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public int? count { get; set; }

        public int? offset { get; set; }

        public bool? include_old { get; set; }

        public bool? include_unified { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetOrderHistoryByInstrumentRequest, GetOrderHistoryByInstrumentRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetOrderHistoryByInstrumentRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }

    public class GetOrderHistoryByInstrumentRequestDto
    {
        public string instrument_name { get; set; }

        public int? count { get; set; }

        public int? offset { get; set; }

        public bool? include_old { get; set; }

        public bool? include_unified { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<GetOrderHistoryByInstrumentRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }
}