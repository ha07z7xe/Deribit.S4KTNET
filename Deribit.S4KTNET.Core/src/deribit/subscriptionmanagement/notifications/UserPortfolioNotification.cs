using System;
using System.Threading;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#user-portfolio-currency

    public class UserPortfolioNotification : SubscriptionNotification<Portfolio>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;
            public Profile()
            {
                this.CreateMap<UserPortfolioNotificationDto, UserPortfolioNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.userportfolio))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<PortfolioDto>), typeof(SubscriptionNotification<Portfolio>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<UserPortfolioNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.userportfolio);
                this.RuleFor(x => x.data).SetValidator(new Portfolio.Validator());
            }
        }
    }

    public class UserPortfolioNotificationDto : SubscriptionNotificationDto<PortfolioDto>
    {
        
    }
}
