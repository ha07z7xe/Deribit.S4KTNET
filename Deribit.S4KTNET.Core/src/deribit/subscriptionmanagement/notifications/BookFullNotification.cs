using AutoMapper;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#book-instrument_name-interval


    public class BookFullNotification : SubscriptionNotification<BookFullData>
    {
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BookFullNotificationDto, BookFullNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.book))
                    .IncludeBase(typeof(SubscriptionNotificationDto<>), typeof(SubscriptionNotification<>));
            }
        }
    }

    public class BookFullNotificationDto : SubscriptionNotificationDto<BookFullDataDto>
    {
        
    }

    public class BookFullData
    {
        public IList<BookFullOrder> asks { get; set; }

        public IList<BookFullOrder> bids { get; set; }

        public long change_id { get; set; }

        public long prev_change_id { get; set; }

        public string instrument_name { get; set; }

        public DateTime timestamp { get; set; }

        // "snapshot" | "change"
        public string type { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BookFullDataDto, BookFullData>()
                    .ForMember(d => d.timestamp, 
                        o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.timestamp));
            }
        }
    }


    public class BookFullDataDto
    {
        public IList<BookFullOrderDto> asks { get; set; }

        public IList<BookFullOrderDto> bids { get; set; }

        public long change_id { get; set; }

        public long prev_change_id { get; set; }

        public string instrument_name { get; set; }

        public long timestamp { get; set; }

        // "snapshot" | "change"
        public string type { get; set; }
    }


    public class BookFullOrder
    {
        public BookFullOrderAction action { get; set; }

        public decimal price { get; set; }

        public decimal amount { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<BookFullOrderDto, BookFullOrder>();
            }
        }

        internal class Validator : AbstractValidator<BookFullOrder>
        {
            public Validator()
            {
                this.RuleFor(x => x.price).NotEmpty();
                this.RuleFor(x => x.action).NotEqual(BookFullOrderAction.unknown);
            }
        }
    }

    public enum BookFullOrderAction
    {
        unknown,
        @new,
        change,
        delete,
    }

    public class BookFullOrderDto
    {
        // "new" | "change" | "delete"
        public string action { get; set; }

        public decimal price { get; set; }

        public decimal amount { get; set; }
    }
}
