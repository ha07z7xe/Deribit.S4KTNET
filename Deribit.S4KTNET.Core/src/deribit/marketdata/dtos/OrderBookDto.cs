using Deribit.S4KTNET.Core.Mapping;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.MarketData
{
    internal class OrderBookDto
    {
        public decimal? ask_iv { get; set; }

        public IList<object[]> asks { get; set; }

        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public decimal? bid_iv { get; set; }

        public IList<object[]> bids { get; set; }

        public decimal? current_funding { get; set; }

        public decimal? delivery_price { get; set; }

        public decimal? funding_8h { get; set; }

        public object greeks { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal last_price { get; set; }

        public decimal? mark_iv { get; set; }

        public decimal mark_price { get; set; }

        public decimal max_price { get; set; }

        public decimal min_price { get; set; }

        public decimal open_interest { get; set; }

        public string state { get; set; }

        public object stats { get; set; }

        public long timestamp { get; set; }

        public decimal underlying_index { get; set; }

        public decimal underlying_price { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<OrderBookDto, OrderBook>()
                    .ForMember(x => x.timestamp, o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp))
                    ;
            }
        }
    }
}