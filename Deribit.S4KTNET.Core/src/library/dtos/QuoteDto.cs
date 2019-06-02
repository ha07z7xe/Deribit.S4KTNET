using Deribit.S4KTNET.Core.Mapping;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core
{
    public class QuoteDto
    {
        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public string instrument_name { get; set; }

        public long timestamp { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<QuoteDto, Quote>()
                    .ForMember(x => x.timestamp, o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp))
                    ;
            }
        }
    }
}