using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#announcements

    public class AnnouncementsNotification : SubscriptionNotification<AnnouncementsData>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;
            public Profile()
            {
                this.CreateMap<AnnouncementsNotificationDto, AnnouncementsNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.announcements))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<AnnouncementsDataDto>), typeof(SubscriptionNotification<AnnouncementsData>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<AnnouncementsNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.announcements);
            }
        }
    }

    public class AnnouncementsNotificationDto : SubscriptionNotificationDto<AnnouncementsDataDto>
    {
        
    }

    public class AnnouncementsData
    {
        public string action { get; set; }

        public string body { get; set; }

        public DateTime date { get; set; }

        public long id { get; set; }

        public bool important { get; set; }

        public long number { get; set; }

        public string title { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<AnnouncementsDataDto, AnnouncementsData>()
                    .ForMember(d => d.date, 
                    o => o.ConvertUsing<UnixTimestampMillisValueConverter, long>(s => s.date));
            }
        }
    }

    public class AnnouncementsDataDto
    {
        public string action { get; set; }

        public string body { get; set; }

        public long date { get; set; }

        public long id { get; set; }

        public bool important { get; set; }

        public long number { get; set; }

        public string title { get; set; }
    }
}
