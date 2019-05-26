using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#trades-instrument_name-interval

    public class TradeNotification : SubscriptionNotification<Trade[]>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<TradeNotificationDto, TradeNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.trades))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<TradeDto[]>), typeof(SubscriptionNotification<Trade[]>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TradeNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.trades);
                this.RuleForEach(x => x.data).SetValidator(new Trade.Validator());
            }
        }
    }

    internal class TradeNotificationDto : SubscriptionNotificationDto<TradeDto[]>
    {
        
    }
}
