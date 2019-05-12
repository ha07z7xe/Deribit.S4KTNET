using System;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    // https://docs.deribit.com/v2/#public-set_heartbeat

    public class SetHeartbeatResponse : ResponseBase
    {
        public bool result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<SetHeartbeatResponseDto, SetHeartbeatResponse>()
                    // todo
                    .ForMember(x => x.result, o => o.MapFrom(x => x.result == "ok"));
            }
        }

    }

    public class SetHeartbeatResponseDto
    {
        public string result { get; set; }
    }
}