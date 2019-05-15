using AutoMapper;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#book-instrument_name-group-depth-interval


    public class BookDepthLimitedNotification : SubscriptionNotification<BookDepthLimitedData>
    {
        public OrderbookGrouping group { get; set; }

        public OrderbookDepth depth { get; set; }

        public Interval interval { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;
            public Profile()
            {
                this.CreateMap<BookDepthLimitedNotificationDto, BookDepthLimitedNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.book))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .ForMember(d => d.group, o => o.Ignore())
                    .ForMember(d => d.depth, o => o.Ignore())
                    .ForMember(d => d.interval, o => o.Ignore())
                    .IncludeBase(typeof(SubscriptionNotificationDto<BookDepthLimitedDataDto>), typeof(SubscriptionNotification<BookDepthLimitedData>))
                    .AfterMap((s, d) =>
                    {
                        var channelpieces = d.channel.Split('.');
                        if (channelpieces.Length != 5)
                            throw new Exception();
                        if (channelpieces[0] != DeribitChannelPrefix.book)
                            throw new Exception();
                        d.group = channelpieces[2].ToOrderbookGrouping();
                        d.depth = channelpieces[3].ToOrderbookDepth();
                        d.interval = channelpieces[4].ToInterval();
                    });
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<BookDepthLimitedNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.data).SetValidator(new BookDepthLimitedData.Validator());
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

        internal class Validator : FluentValidation.AbstractValidator<BookDepthLimitedData>
        {
            public Validator()
            {
                var v2 = new BookDepthLimitedOrder.Validator();
                this.RuleForEach(x => x.asks).SetValidator(v2);
                this.RuleForEach(x => x.bids).SetValidator(v2);
            }
        }
    }


    public class BookDepthLimitedDataDto
    {
        public IList<object[]> asks { get; set; }

        public IList<object[]> bids { get; set; }

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
                this.CreateMap<object[], BookDepthLimitedOrder>()
                    .ForMember(d => d.price, o => o.MapFrom(s => s[0]))
                    .ForMember(d => d.amount, o => o.MapFrom(s => s[1]));
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
}
