using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-buy

    public class BuySellRequest : RequestBase
    {
        public string instrument_name { get; set; }

        public decimal amount { get; set; }

        public OrderType type { get; set; }

        public string label { get; set; }

        public decimal? price { get; set; }

        public OrderTimeInForce? time_in_force { get; set; }

        public decimal? max_show { get; set; }

        public bool post_only { get; set; }

        public bool reduce_only { get; set; }

        public decimal? stop_price { get; set; }

        public OrderTriggerType? trigger { get; set; }

        public string advanced { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BuySellRequest, BuySellRequestDto>()
                    .ForMember(d => d.type, o => o.MapFrom(s => s.type.ToDeribitString()))
                    .ForMember(d => d.time_in_force, o => o.MapFrom(s => s.time_in_force.HasValue ? s.time_in_force.Value.ToDeribitString() : null))
                    .ForMember(d => d.trigger, o => o.MapFrom(s => s.trigger.HasValue ? s.trigger.Value.ToDeribitString() : null))
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<BuySellRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.price).NotEmpty().When(x => x.type == OrderType.limit || x.type == OrderType.stop_limit);
                this.RuleFor(x => x.price).Empty().When(x => x.type == OrderType.market || x.type == OrderType.stop_market);
                this.RuleFor(x => x.amount).GreaterThan(0);
                this.When(x => x.instrument_name == DeribitInstruments.Perpetual.BTCPERPETUAL, () =>
                {
                    this.RuleFor(x => x.amount)
                        .GreaterThanOrEqualTo(DeribitInstruments.ByInstrumentName[DeribitInstruments.Perpetual.BTCPERPETUAL].ContractSize);
                    this.RuleFor(x => x.amount)
                        .Must(x => x % DeribitInstruments.ByInstrumentName[DeribitInstruments.Perpetual.BTCPERPETUAL].ContractSize == 0);
                });
                this.When(x => x.instrument_name == DeribitInstruments.Perpetual.ETHPERPETUAL, () =>
                {
                    this.RuleFor(x => x.amount)
                        .GreaterThanOrEqualTo(DeribitInstruments.ByInstrumentName[DeribitInstruments.Perpetual.ETHPERPETUAL].ContractSize);
                    this.RuleFor(x => x.amount)
                        .Must(x => x % DeribitInstruments.ByInstrumentName[DeribitInstruments.Perpetual.ETHPERPETUAL].ContractSize == 0);
                });
                this.RuleFor(x => x.stop_price).NotEmpty().When(x => x.type == OrderType.stop_limit || x.type == OrderType.stop_market);
                this.RuleFor(x => x.stop_price).Empty().When(x => x.type == OrderType.limit|| x.type == OrderType.market);
                this.RuleFor(x => x.trigger).NotEmpty().When(x => x.type == OrderType.stop_limit || x.type == OrderType.stop_market);
            }
        }
    }

    public class BuySellRequestDto
    {
        public string instrument_name { get; set; }

        public decimal amount { get; set; }

        public string type { get; set; }

        public string label { get; set; }

        public decimal? price { get; set; }

        public string time_in_force { get; set; }

        public decimal? max_show { get; set; }

        public bool post_only { get; set; }

        public bool reduce_only { get; set; }

        public decimal? stop_price { get; set; }

        public string trigger { get; set; }

        public string advanced { get; set; }
    }
}