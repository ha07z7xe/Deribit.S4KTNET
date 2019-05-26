using System;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    public class SubscriptionNotification<TData> //where TData : new()
    {
        public string channelprefix { get; set; }

        public string channel { get; set; }

        /// <summary>
        /// timestamp received
        /// </summary>
        public DateTime timestamp { get; set; }

        public int sequencenumber { get; set; }

        public TData data { get; set; }
    }

    public class SubscriptionNotificationDto<TData> //where TData : new()
    {
        public string channel { get; set; }

        public TData data { get; set; }
    }
}
