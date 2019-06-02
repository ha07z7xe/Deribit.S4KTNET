using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core
{
    public class OrderDto
    {
        public string advanced { get; set; }

        public decimal amount { get; set; }

        public bool api { get; set; }

        public decimal average_price { get; set; }

        public decimal commission { get; set; }

        public long creation_timestamp { get; set; }

        public string direction { get; set; }

        public decimal filled_amount { get; set; }

        public decimal implv { get; set; }

        public string instrument_name { get; set; }

        public bool is_liquidation { get; set; }

        public string label { get; set; }

        public long last_update_timestamp { get; set; }

        public decimal max_show { get; set; }

        public string order_id { get; set; }

        public string order_state { get; set; }

        public string order_type { get; set; }

        public string original_order_type { get; set; }

        public bool post_only { get; set; }

        public string price { get; set; } // can be "market_price"

        public decimal profit_loss { get; set; }

        public bool reduce_only { get; set; }

        public decimal stop_price { get; set; }

        public string time_in_force { get; set; }

        public string trigger { get; set; }

        public bool triggered { get; set; }

        public decimal usd { get; set; }

        
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<OrderDto, Order>()
                    .ForMember(d => d.direction, o => o.MapFrom(s => s.direction.ToBuySell()))
                    .ForMember(d => d.order_state, o => o.MapFrom(s => s.order_state.ToOrderState()))
                    .ForMember(d => d.order_type, o => o.MapFrom(s => s.order_type.ToOrderType()))
                    .ForMember(d => d.original_order_type, o => o.MapFrom(s => s.original_order_type.ToOrderType()))
                    .ForMember(d => d.time_in_force, o => o.MapFrom(s => s.time_in_force.ToOrderTimeInForce()))
                    .ForMember(d => d.trigger, o => o.MapFrom(s => s.trigger.ToOrderTriggerType()))
                    .ForMember(d => d.last_update_timestamp, o => o.MapFrom(s => s.last_update_timestamp.UnixTimeStampMillisToDateTimeUtc()))
                    .ForMember(d => d.creation_timestamp, o => o.MapFrom(s => s.creation_timestamp.UnixTimeStampMillisToDateTimeUtc()))
                    .ForMember(d => d.price, o => o.MapFrom(s => (s.price == null || s.price == "market_price") 
                        ? (decimal?) null : decimal.Parse(s.price)));
                    ;
            }
        }

    }
}