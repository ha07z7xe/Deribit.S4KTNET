using FluentValidation;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    // https://docs.deribit.com/v2/#public-disable_heartbeat

    public class DisableHeartbeatRequest : RequestBase
    {
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<DisableHeartbeatRequest, DisableHeartbeatRequestDto>();
            }
        }
    }

    public class DisableHeartbeatRequestDto
    {
        
    }
}