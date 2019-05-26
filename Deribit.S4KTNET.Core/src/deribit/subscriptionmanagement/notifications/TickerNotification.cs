using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using Deribit.S4KTNET.Core.MarketData;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#ticker-instrument_name-interval

    public class TickerNotification : SubscriptionNotification<Ticker>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<TickerNotificationDto, TickerNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.ticker))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<TickerDto>), typeof(SubscriptionNotification<Ticker>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<TickerNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.ticker);
                this.RuleFor(x => x.data).SetValidator(new Ticker.Validator());
            }
        }
    }

    internal class TickerNotificationDto : SubscriptionNotificationDto<TickerDto>
    {
        
    }
}
