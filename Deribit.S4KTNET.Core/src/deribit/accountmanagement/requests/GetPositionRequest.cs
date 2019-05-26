using FluentValidation;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    // https://docs.deribit.com/v2/#private-get_position 

    public class GetPositionRequest : RequestBase
    {
        public string instrument_name { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetPositionRequest, GetPositionRequestDto>();
            }
        }

        internal class Validator : AbstractValidator<GetPositionRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }

    internal class GetPositionRequestDto
    {
        public string instrument_name { get; set; }
    }
}