using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/#public-get_tradingview_chart_data

    public class GetTradingViewChartDataRequest : RequestBase
    {
        public string instrument_name { get; set; }
        public DateTime start_timestamp { get; set; }
        public DateTime end_timestamp { get; set; }
        public int resolution { get; set; }



        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetTradingViewChartDataRequest, GetTradingviewChartDataRequestDto>()
                    .ForMember(x => x.start_timestamp, o => o.ConvertUsing<UnixTimestampMillisValueConverter, DateTime>(s => s.start_timestamp))
                    .ForMember(x => x.end_timestamp, o => o.ConvertUsing<UnixTimestampMillisValueConverter, DateTime>(s => s.end_timestamp))
                    .ForMember(x => x.resolution, o => o.ToString());

            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetTradingViewChartDataRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
                this.RuleFor(x => x.start_timestamp).NotEmpty();
                this.RuleFor(x => x.end_timestamp).NotEmpty();
                this.RuleFor(x => x.resolution).NotEmpty();
            }
        }
    }

    public class GetTradingviewChartDataRequestDto
    {
        public string instrument_name { get; set; }
        public long start_timestamp { get; set; }
        public long end_timestamp { get; set; }
        public string resolution { get; set; }
    }
}