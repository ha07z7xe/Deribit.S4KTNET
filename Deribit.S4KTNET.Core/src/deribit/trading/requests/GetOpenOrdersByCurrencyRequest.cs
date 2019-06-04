using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_open_orders_by_currency

    public class GetOpenOrdersByCurrencyRequest : RequestBase
    {
        public CurrencyCode currency { get; set; }

        public InstrumentKind? kind { get; set; }

        public string type { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetOpenOrdersByCurrencyRequest, GetOpenOrdersByCurrencyRequestDto>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToDeribitString()))
                    .ForMember(d => d.kind, o => 
                    {
                        o.PreCondition(s => s.kind.HasValue);
                        o.MapFrom(s => s.kind.Value.ToDeribitString());
                    })
                    .AfterMap((s, d, r) =>
                    {
                        if (d.kind == "any")
                            d.kind = null;
                    });
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetOpenOrdersByCurrencyRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().NotEqual(CurrencyCode.any);
                this.RuleFor(x => x.type).Must(x => x == null || x == "all" || x == "limit"
                    || x == "stop_all" || x == "stop_limit" || x == "stop_market")
                    .WithMessage(x => @"type == [ ""all"" | ""limit"" | ""stop_all"" | ""stop_limit"" | ""stop_market"" ]");
                ;
            }
        }
    }

    public class GetOpenOrdersByCurrencyRequestDto
    {
        public string currency { get; set; }

        public string kind { get; set; }

        public string type { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<GetOpenOrdersByCurrencyRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().Must(x => x == "BTC" || x == "ETH");
                this.RuleFor(x => x.kind).Must(x => x == null || x == "future" || x == "option");
                this.RuleFor(x => x.type).Must(x => x == null || x == "all" || x == "limit"
                    || x == "stop_all" || x == "stop_limit" || x == "stop_market");
            }
        }
    }
}