using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using Deribit.S4KTNET.Core.MarketData;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#quote-instrument_name

    public class QuoteNotification : SubscriptionNotification<Quote>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<QuoteNotificationDto, QuoteNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.quote))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<QuoteDto>), typeof(SubscriptionNotification<Quote>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<QuoteNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.quote);
                this.RuleFor(x => x.data).SetValidator(new Quote.Validator());
            }
        }
    }

    internal class QuoteNotificationDto : SubscriptionNotificationDto<QuoteDto>
    {
        
    }
}
