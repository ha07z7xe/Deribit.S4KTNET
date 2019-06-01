using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    public class PositionDto
    {
        public decimal average_price { get; set; }

        public decimal? average_price_usd { get; set; }

        public decimal delta { get; set; }

        public string direction { get; set; }

        public decimal? estimated_liquidation_price { get; set; }

        public decimal floating_profit_loss { get; set; }
        
        public decimal? floating_profit_loss_usd { get; set; }

        public decimal index_price { get; set; }

        public decimal initial_margin { get; set; }

        public string instrument_name { get; set; }

        public string kind { get; set; }

        public decimal maintenance_margin { get; set; }

        public decimal mark_price { get; set; }

        public decimal open_orders_margin { get; set; }

        public decimal realized_profit_loss { get; set; }

        public decimal settlement_price { get; set; }

        public decimal size { get; set; }

        public decimal? size_currency { get; set; }

        public decimal total_proft_loss { get; set; }

        
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<PositionDto, Position>()
                    .ForMember(d => d.direction, o => o.MapFrom(s => s.direction.ToBuySell()))
                    .ForMember(d => d.kind, o => o.MapFrom(s => s.kind.ToInstrumentKind()))
                    ;
            }
        }
    }
}