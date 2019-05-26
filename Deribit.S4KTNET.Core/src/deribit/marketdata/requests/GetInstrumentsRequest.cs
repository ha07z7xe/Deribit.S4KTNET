using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_index

    public class GetInstrumentsRequest : RequestBase
    {
        public CurrencyCode currency { get; set; }

        public InstrumentKind? kind { get; set; }

        public bool expired { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetInstrumentsRequest, GetInstrumentsRequestDto>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToDeribitString()))
                    .ForMember(d => d.kind, o => o.MapFrom(s => s.kind.HasValue ? s.kind.Value.ToDeribitString() : null))
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetInstrumentsRequest>
        {
            public Validator()
            {
                //this.RuleFor(x => x.currency).NotEmpty();
            }
        }
    }

    internal class GetInstrumentsRequestDto
    {
        public string currency { get; set; }

        public string kind { get; set; }

        public bool expired { get; set; }
    }
}