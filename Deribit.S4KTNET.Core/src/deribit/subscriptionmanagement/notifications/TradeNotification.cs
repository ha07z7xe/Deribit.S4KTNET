using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#trades-instrument_name-interval

    public class TradeNotification : SubscriptionNotification<TradeData[]>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<TradeNotificationDto, TradeNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.trades))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<TradeDataDto[]>), typeof(SubscriptionNotification<TradeData[]>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TradeNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.trades);
                this.RuleForEach(x => x.data).SetValidator(new TradeData.Validator());
            }
        }
    }

    public class TradeNotificationDto : SubscriptionNotificationDto<TradeDataDto[]>
    {
        
    }

    public class TradeData
    {
        public decimal amount { get; set; }

        public BuySell direction { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal iv { get; set; }

        public decimal price { get; set; }

        public TickDirection tick_direction { get; set; }

        public DateTime timestamp { get; set; }

        public string trade_id { get; set; }

        public int trade_seq { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TradeDataDto, TradeData>()
                    .ForMember(d => d.direction, o => o.MapFrom(s => s.direction.ToBuySell()))
                    .ForMember(d => d.timestamp, 
                    o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TradeData>
        {
            public Validator()
            {
                this.RuleFor(x => x.amount).GreaterThan(0);
                this.RuleFor(x => x.price).GreaterThan(0);
            }
        }
    }

    public class TradeDataDto
    {
        public decimal amount { get; set; }

        public string direction { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal iv { get; set; }

        public decimal price { get; set; }

        public int tick_direction { get; set; }

        public long timestamp { get; set; }

        public string trade_id { get; set; }

        public int trade_seq { get; set; }
    }
}
