using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core
{
    public class TradeDto
    {
        public decimal amount { get; set; }

        public string direction { get; set; }

        public decimal fee { get; set; }

        public string fee_currency { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal iv { get; set; }

        public string label { get; set; }

        public string liquidity { get; set; }

        public string matching_id { get; set; }

        public string order_id { get; set; }

        public string order_type { get; set; }

        public decimal price { get; set; }

        public bool self_trade { get; set; }

        public string state { get; set; }

        public int tick_direction { get; set; }

        public long timestamp { get; set; }

        public string trade_id { get; set; }

        public long trade_seq { get; set; }


        
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TradeDto, Trade>()
                    .ForMember(d => d.direction, o => o.MapFrom(s => s.direction.ToBuySell()))
                    .ForMember(d => d.order_type, o => o.MapFrom(s => s.order_type.ToOrderType()))
                    .ForMember(d => d.state, o => o.MapFrom(s => s.state.ToOrderState()))
                    .ForMember(d => d.timestamp, o => o.MapFrom(s => s.timestamp.UnixTimeStampMillisToDateTimeUtc()))
                    ;
            }
        }

    }
}