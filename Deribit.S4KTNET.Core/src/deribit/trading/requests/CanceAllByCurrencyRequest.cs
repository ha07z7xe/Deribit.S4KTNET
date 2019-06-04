using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-cancel_all_by_currency

    public class CancelAllByCurrencyRequest : RequestBase
    {
        public CurrencyCode currency { get; set; }

        public InstrumentKind? kind { get; set; }

        public string type { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<CancelAllByCurrencyRequest, CancelAllByCurrencyRequestDto>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToDeribitString()))
                    .ForMember(d => d.kind, o =>
                    {
                        o.PreCondition(s => s.kind.HasValue);
                        o.MapFrom(s => s.kind.Value.ToDeribitString());
                    });
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<CancelAllByCurrencyRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.type)
                    .Must(x => x == null || x == "all" || x == "limit" || x == "stop")
                    .WithMessage(x => @"type == [ ""all"" | ""limit"" | ""stop"" ]");
                ;
            }
        }
    }

    public class CancelAllByCurrencyRequestDto
    {
        public string currency { get; set; }

        public string kind { get; set; }

        public string type { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<CancelAllByCurrencyRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).Must(x => x == "BTC" || x == "ETH");
                this.RuleFor(x => x.kind).Must(x => x == null || x == "future" || x == "option");
                this.RuleFor(x => x.type).Must(x => x == null || x == "all" || x == "limit" || x == "stop");
            }
        }
    }
}