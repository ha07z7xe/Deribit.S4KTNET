using Deribit.S4KTNET.Core.Mapping;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core
{
    public class TradingViewChartDataDto
    {
        public IList<decimal> volume { get; set; }
        public IList<decimal> cost { get; set; }
        public IList<long> ticks { get; set; }
        public string status { get; set; }
        public IList<decimal> open { get; set; }
        public IList<decimal> low { get; set; }
        public IList<decimal> high { get; set; }
        public IList<decimal> close { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TradingViewChartDataDto, TradingViewChartData>()
                    .ForMember(x => x.timestamps, o => o.ConvertUsing<ArrayUnixTimestampMillisValueConveter, IList<long>>(s => s.ticks))
                    ;
            }
        }
    }
}