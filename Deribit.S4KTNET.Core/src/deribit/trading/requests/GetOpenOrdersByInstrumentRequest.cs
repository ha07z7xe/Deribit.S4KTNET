using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_open_orders_by_currency

    public class GetOpenOrdersByInstrumentRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public string type { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetOpenOrdersByInstrumentRequest, GetOpenOrdersByInstrumentRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetOpenOrdersByInstrumentRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.type).Must(x => x == null || x == "all" || x == "limit"
                    || x == "stop_all" || x == "stop_limit" || x == "stop_market");
            }
        }
    }

    public class GetOpenOrdersByInstrumentRequestDto
    {
        public string instrument_name { get; set; }

        public string type { get; set; }
    }
}