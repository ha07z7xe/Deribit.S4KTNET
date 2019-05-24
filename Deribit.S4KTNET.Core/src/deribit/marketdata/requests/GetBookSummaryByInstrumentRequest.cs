using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_book_summary_by_instrument

    public class GetBookSummaryByInstrumentRequest : RequestBase
    {
        public string instrument_name { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetBookSummaryByInstrumentRequest, GetBookSummaryByInstrumentRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetBookSummaryByInstrumentRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }

    public class GetBookSummaryByInstrumentRequestDto
    {
        public string instrument_name { get; set; }
    }
}