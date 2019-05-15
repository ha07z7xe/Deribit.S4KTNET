using AutoMapper;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#book-instrument_name-interval


    public class BookFullNotification : SubscriptionNotification<BookFullData>
    {
        public Interval interval { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;
            public Profile()
            {
                this.CreateMap<BookFullNotificationDto, BookFullNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.book))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .ForMember(d => d.interval, o => o.Ignore())
                    .IncludeBase(typeof(SubscriptionNotificationDto<BookFullDataDto>), typeof(SubscriptionNotification<BookFullData>))
                    .AfterMap((s, d) =>
                    {
                        var channelpieces = d.channel.Split('.');
                        if (channelpieces.Length != 3)
                            throw new Exception();
                        if (channelpieces[0] != DeribitChannelPrefix.book)
                            throw new Exception();
                        d.interval = channelpieces[2].ToInterval();
                    });
            }
        }

        internal class Validator : AbstractValidator<BookFullNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.data).SetValidator(new BookFullData.Validator());
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

        internal class Validator : AbstractValidator<BookFullData>
        {
            public Validator()
            {
                var v2 = new BookFullOrder.Validator();
                this.RuleForEach(x => x.asks).SetValidator(v2);
                this.RuleForEach(x => x.bids).SetValidator(v2);
            }
        }
    }


    public class BookFullDataDto
    {
        public IList<object[]> asks { get; set; }

        public IList<object[]> bids { get; set; }

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
                this.CreateMap<object[], BookFullOrder>()
                    .ForMember(d => d.action, o => o.MapFrom(s => Enum.Parse(typeof(BookFullOrderAction), (string) s[0])))
                    .ForMember(d => d.price, o => o.MapFrom(s => s[1]))
                    .ForMember(d => d.amount, o => o.MapFrom(s => s[2]));
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
}
