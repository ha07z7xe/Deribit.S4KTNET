using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#ticker-instrument_name-interval

    public class TickerNotification : SubscriptionNotification<TickerData>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<TickerNotificationDto, TickerNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.quote))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<TickerDataDto>), typeof(SubscriptionNotification<TickerData>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TickerNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.data).SetValidator(new TickerData.Validator());
            }
        }
    }

    public class TickerNotificationDto : SubscriptionNotificationDto<TickerDataDto>
    {
        
    }

    public class TickerData
    {
        public decimal ask_iv { get; set; }
        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public decimal bid_iv { get; set; }

        public decimal current_funding { get; set; }

        public decimal delivery_price { get; set; }

        public decimal funding_8h { get; set; }

        public object greeks { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal interest_rate { get; set; }

        public decimal last_price { get; set; }

        public decimal mark_iv { get; set; }

        public decimal mark_price { get; set; }

        public decimal max_price { get; set; }

        public decimal min_price { get; set; }

        public decimal open_interest { get; set; }

        public decimal settlement_price { get; set; }

        public string state { get; set; }

        public object stats { get; set; }

        public DateTime timestamp { get; set; }

        public decimal underlying_index { get; set; }

        public decimal underlying_price { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TickerDataDto, TickerData>()
                    .ForMember(d => d.timestamp, 
                    o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TickerData>
        {
            public Validator()
            {
                this.RuleFor(x => x.best_ask_amount).GreaterThan(0);
                this.RuleFor(x => x.best_bid_amount).GreaterThan(0);
                this.RuleFor(x => x.best_ask_price).GreaterThan(x => x.best_bid_price);
                this.RuleFor(x => x.best_bid_price).GreaterThan(0);
                this.RuleFor(x => x.best_ask_price).GreaterThan(0);
            }
        }
    }

    public class TickerDataDto
    {
        public decimal ask_iv { get; set; }
        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public decimal bid_iv { get; set; }

        public decimal current_funding { get; set; }

        public decimal delivery_price { get; set; }

        public decimal funding_8h { get; set; }

        public object greeks { get; set; }

        public decimal index_price { get; set; }

        public string instrument_name { get; set; }

        public decimal interest_rate { get; set; }

        public decimal last_price { get; set; }

        public decimal mark_iv { get; set; }

        public decimal mark_price { get; set; }

        public decimal max_price { get; set; }

        public decimal min_price { get; set; }

        public decimal open_interest { get; set; }

        public decimal settlement_price { get; set; }

        public string state { get; set; }

        public object stats { get; set; }

        public long timestamp { get; set; }

        public decimal underlying_index { get; set; }

        public decimal underlying_price { get; set; }
    }
}
