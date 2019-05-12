using System;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    // https://docs.deribit.com/v2/#public-disable_heartbeat

    public class DisableHeartbeatResponse : ResponseBase
    {
        public bool result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<DisableHeartbeatResponseDto, DisableHeartbeatResponse>()
                    // todo
                    .ForMember(x => x.result, o => o.MapFrom(x => x.result == "ok"));
            }
        }

    }

    public class DisableHeartbeatResponseDto
    {
        public string result { get; set; }
    }
}