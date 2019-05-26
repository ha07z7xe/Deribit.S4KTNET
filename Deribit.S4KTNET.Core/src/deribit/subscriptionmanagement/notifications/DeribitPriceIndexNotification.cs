using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#deribit_price_index-index_name

    public class DeribitPriceIndexNotification : SubscriptionNotification<DeribitPriceIndexData>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;
            public Profile()
            {
                this.CreateMap<DeribitPriceIndexNotificationDto, DeribitPriceIndexNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.deribit_price_index))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<DeribitPriceIndexDataDto>), typeof(SubscriptionNotification<DeribitPriceIndexData>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<DeribitPriceIndexNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.deribit_price_index);
            }
        }
    }

    internal class DeribitPriceIndexNotificationDto : SubscriptionNotificationDto<DeribitPriceIndexDataDto>
    {
        
    }

    public class DeribitPriceIndexData
    {
        public string index_name { get; set; }

        public decimal price { get; set; }

        public DateTime timestamp { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<DeribitPriceIndexDataDto, DeribitPriceIndexData>()
                    .ForMember(d => d.timestamp, 
                    o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp));
            }
        }
    }

    internal class DeribitPriceIndexDataDto
    {
        public string index_name { get; set; }

        public decimal price { get; set; }

        public long timestamp { get; set; }
    }
}
