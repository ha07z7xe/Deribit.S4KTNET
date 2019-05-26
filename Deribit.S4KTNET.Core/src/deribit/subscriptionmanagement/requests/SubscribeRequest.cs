using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#public-subscribe

    public class SubscribeRequest : RequestBase
    {
        public string[] channels { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<SubscribeRequest, SubscribeRequestDto>();
            }
        }

        internal class Validator : AbstractValidator<SubscribeRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.channels).NotEmpty();
            }
        }
    }

    public class SubscribeRequestDto
    {
        public string[] channels { get; set; }
    }
}