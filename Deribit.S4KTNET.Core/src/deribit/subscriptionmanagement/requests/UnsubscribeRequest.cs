using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#public-subscribe

    public class UnsubscribeRequest : RequestBase
    {
        public string[] channels { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<UnsubscribeRequest, UnsubscribeRequestDto>();
            }
        }

        internal class Validator : AbstractValidator<UnsubscribeRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.channels).NotEmpty();
            }
        }
    }

    public class UnsubscribeRequestDto
    {
        public string[] channels { get; set; }
    }
}