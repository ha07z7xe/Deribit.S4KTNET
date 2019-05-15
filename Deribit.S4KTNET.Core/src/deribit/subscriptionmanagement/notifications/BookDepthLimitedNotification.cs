using AutoMapper;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#book-instrument_name-group-depth-interval


    public class BookDepthLimitedNotification : SubscriptionNotification<BookDepthLimitedData>
    {
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BookDepthLimitedNotificationDto, BookDepthLimitedNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.book))
                    .IncludeBase(typeof(SubscriptionNotificationDto<>), typeof(SubscriptionNotification<>));
            }
        }
    }

    public class BookDepthLimitedNotificationDto : SubscriptionNotificationDto<BookDepthLimitedDataDto>
    {
        
    }

    public class BookDepthLimitedData
    {
        public IList<BookDepthLimitedOrder> asks { get; set; }

        public IList<BookDepthLimitedOrder> bids { get; set; }

        public long change_id { get; set; }

        public string instrument_name { get; set; }

        public DateTime timestamp { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BookDepthLimitedDataDto, BookDepthLimitedData>()
                    .ForMember(d => d.timestamp, 
                        o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp));
            }
        }
    }


    public class BookDepthLimitedDataDto
    {
        public IList<BookDepthLimitedOrderDto> asks { get; set; }

        public IList<BookDepthLimitedOrderDto> bids { get; set; }

        public long change_id { get; set; }

        public string instrument_name { get; set; }

        public long timestamp { get; set; }
    }


    public class BookDepthLimitedOrder
    {
        public decimal price { get; set; }

        public decimal amount { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BookDepthLimitedOrderDto, BookDepthLimitedOrder>();
            }
        }

        internal class Validator : AbstractValidator<BookDepthLimitedOrder>
        {
            public Validator()
            {
                this.RuleFor(x => x.price).NotEmpty();
            }
        }
    }

    public class BookDepthLimitedOrderDto
    {
        public decimal price { get; set; }

        public decimal amount { get; set; }
    }
}
