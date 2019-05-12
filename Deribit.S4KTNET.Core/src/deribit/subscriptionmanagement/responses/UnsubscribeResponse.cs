using System;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#public-subscribe

    public class UnsubscribeResponse : ResponseBase
    {
        public string[] subscribed_channels { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<string[], UnsubscribeResponse>()
                    .ForMember(d => d.subscribed_channels, o => o.MapFrom(s => s));
            }
        }
    }
}