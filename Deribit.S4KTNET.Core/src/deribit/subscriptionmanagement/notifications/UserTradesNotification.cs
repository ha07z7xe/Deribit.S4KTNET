using System.Threading;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#user-trades-instrument_name-interval

    public class UserTradesNotification : SubscriptionNotification<Trade[]>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<UserTradesNotificationDto, UserTradesNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.usertrades))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<TradeDto[]>), typeof(SubscriptionNotification<Trade[]>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<UserTradesNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.usertrades);
                this.RuleForEach(x => x.data).SetValidator(new Trade.Validator());
            }
        }
    }

    public class UserTradesNotificationDto : SubscriptionNotificationDto<TradeDto[]>
    {
        
    }
}
