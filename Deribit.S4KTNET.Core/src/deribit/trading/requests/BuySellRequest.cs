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

        public decimal price { get; set; }

        public OrderTimeInForce time_in_force { get; set; }

        public decimal max_show { get; set; }

        public bool post_only { get; set; }

        public bool reduce_only { get; set; }

        public decimal stop_price { get; set; }

        public OrderTriggerType trigger { get; set; }

        public string advanced { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BuySellRequest, BuySellRequestDto>()
                    .ForMember(d => d.type, o => o.MapFrom(s => s.type.ToDeribitString()))
                    .ForMember(d => d.time_in_force, o => o.MapFrom(s => s.time_in_force.ToDeribitString()))
                    .ForMember(d => d.trigger, o => o.MapFrom(s => s.trigger.ToDeribitString()))
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<BuySellRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.price).NotEmpty().When(x => x.type == OrderType.limit || x.type == OrderType.stop_limit);
                this.RuleFor(x => x.amount).GreaterThan(0);
                this.RuleFor(x => x.stop_price).NotEmpty().When(x => x.type == OrderType.stop_limit || x.type == OrderType.stop_market);
            }
        }
    }

    public class BuySellRequestDto
    {
        public string instrument_name { get; set; }

        public decimal amount { get; set; }

        public string type { get; set; }

        public string label { get; set; }

        public decimal price { get; set; }

        public string time_in_force { get; set; }

        public decimal max_show { get; set; }

        public bool post_only { get; set; }

        public bool reduce_only { get; set; }

        public decimal stop_price { get; set; }

        public string trigger { get; set; }

        public string advanced { get; set; }
    }
}