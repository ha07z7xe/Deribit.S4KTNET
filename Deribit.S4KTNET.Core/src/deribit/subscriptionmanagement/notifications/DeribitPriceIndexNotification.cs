using System;
using Deribit.S4KTNET.Core.Mapping;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#deribit_price_index-index_name

    public class DeribitPriceIndexNotification : SubscriptionNotification<DeribitPriceIndexData>
    {
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<DeribitPriceIndexNotificationDto, DeribitPriceIndexNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.deribit_price_index))
                    .IncludeBase(typeof(SubscriptionNotificationDto<>), typeof(SubscriptionNotification<>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<DeribitPriceIndexNotification>
        {

        }
    }

    public class DeribitPriceIndexNotificationDto : SubscriptionNotificationDto<DeribitPriceIndexDataDto>
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

    public class DeribitPriceIndexDataDto
    {
        public string index_name { get; set; }

        public decimal price { get; set; }

        public long timestamp { get; set; }
    }
}
