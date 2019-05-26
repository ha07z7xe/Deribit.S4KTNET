using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-cancel

    public class CancelRequest : RequestBase
    {
        public string order_id { get; set; }
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<CancelRequest, CancelRequestDto>()
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<CancelRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.order_id).NotEmpty();
            }
        }
    }

    internal class CancelRequestDto
    {
        public string order_id { get; set; }
    }
}