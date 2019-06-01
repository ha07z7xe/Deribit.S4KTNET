using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_order_history_by_currency

    public class GetOrderHistoryByCurrencyRequest : RequestBase
    {
        public CurrencyCode currency { get; set; }

        public InstrumentKind? kind { get; set; }

        public int? count { get; set; }

        public int? offset { get; set; }

        public bool? include_old { get; set; }

        public bool? include_unified { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetOrderHistoryByCurrencyRequest, GetOrderHistoryByCurrencyRequestDto>()
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

        internal class Validator : FluentValidation.AbstractValidator<GetOrderHistoryByCurrencyRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().NotEqual(CurrencyCode.any);
            }
        }
    }

    public class GetOrderHistoryByCurrencyRequestDto
    {
        public string currency { get; set; }

        public string kind { get; set; }

        public int? count { get; set; }

        public int? offset { get; set; }

        public bool? include_old { get; set; }

        public bool? include_unified { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<GetOrderHistoryByCurrencyRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().Must(x => x == "BTC" || x == "ETH");
                this.RuleFor(x => x.kind).Must(x => x == null || x == "future" || x == "option");
            }
        }
    }
}