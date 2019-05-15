namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    internal class NotificationsMappingProfile : AutoMapper.Profile
    {
        public NotificationsMappingProfile()
        {
            this.CreateMap(typeof(SubscriptionNotificationDto<>), typeof(SubscriptionNotification<>))
                .ForMember(nameof(SubscriptionNotification<byte>.channelprefix), o => o.Ignore());
        }
    }
}
