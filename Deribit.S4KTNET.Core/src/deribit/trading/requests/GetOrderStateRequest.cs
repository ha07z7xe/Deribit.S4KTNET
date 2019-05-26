using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_order_state

    public class GetOrderStateRequest : RequestBase
    {
        public string order_id { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetOrderStateRequest, GetOrderStateRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetOrderStateRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.order_id).NotEmpty();
            }
        }
    }

    internal class GetOrderStateRequestDto
    {
        public string order_id { get; set; }
    }
}