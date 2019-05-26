using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_last_trades_by_instrument

    public class GetLastTradesByInstrumentRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public int? start_seq { get; set; }

        public int? end_seq { get; set; }

        public int? count { get; set; }

        public bool? include_old { get; set; }

        public string sorting { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetLastTradesByInstrumentRequest, GetLastTradesByInstrumentRequestDto>()
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetLastTradesByInstrumentRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }

    internal class GetLastTradesByInstrumentRequestDto
    {
        public string instrument_name { get; set; }

        public int? start_seq { get; set; }

        public int? end_seq { get; set; }

        public int? count { get; set; }

        public bool? include_old { get; set; }

        public string sorting { get; set; }
    }
}