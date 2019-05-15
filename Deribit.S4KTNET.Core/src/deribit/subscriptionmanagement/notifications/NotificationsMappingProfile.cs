using System;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    internal class NotificationsMappingProfile : AutoMapper.Profile
    {
        public NotificationsMappingProfile()
        {
            this.CreateMap(typeof(SubscriptionNotificationDto<>), typeof(SubscriptionNotification<>))
                .ForMember(nameof(SubscriptionNotification<byte>.channelprefix), o => o.Ignore())
                .ForMember(nameof(SubscriptionNotification<byte>.sequencenumber), o => o.Ignore())
                .ForMember(nameof(SubscriptionNotification<byte>.timestamp), o => o.MapFrom(s => DateTime.UtcNow))
                ;
        }
    }
}
