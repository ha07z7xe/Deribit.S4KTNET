using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#quote-instrument_name

    public class QuoteNotification : SubscriptionNotification<QuoteData>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<QuoteNotificationDto, QuoteNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.quote))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<QuoteDataDto>), typeof(SubscriptionNotification<QuoteData>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<QuoteNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.quote);
                this.RuleFor(x => x.data).SetValidator(new QuoteData.Validator());
            }
        }
    }

    public class QuoteNotificationDto : SubscriptionNotificationDto<QuoteDataDto>
    {
        
    }

    public class QuoteData
    {
        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public string instrument_name { get; set; }

        public DateTime timestamp { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<QuoteDataDto, QuoteData>()
                    .ForMember(d => d.timestamp, 
                    o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<QuoteData>
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

    public class QuoteDataDto
    {
        public decimal best_ask_amount { get; set; }

        public decimal best_ask_price { get; set; }

        public decimal best_bid_amount { get; set; }

        public decimal best_bid_price { get; set; }

        public string instrument_name { get; set; }

        public long timestamp { get; set; }
    }
}
