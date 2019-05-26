using FluentValidation;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    // https://docs.deribit.com/v2/#public-set_heartbeat

    public class SetHeartbeatRequest : RequestBase
    {
        public int interval { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<SetHeartbeatRequest, SetHeartbeatRequestDto>();
            }
        }

        internal class Validator : AbstractValidator<SetHeartbeatRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.interval).GreaterThanOrEqualTo(10);
            }
        }
    }

    public class SetHeartbeatRequestDto
    {
        public int interval { get; set; }
    }
}