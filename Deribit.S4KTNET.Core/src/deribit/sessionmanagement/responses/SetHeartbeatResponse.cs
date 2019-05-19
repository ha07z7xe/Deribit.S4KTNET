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
                    .ForMember(x => x.result, o => o.MapFrom(x => x.result == "ok"));

                this.CreateMap<string, SetHeartbeatResponse>()
                    .ConvertUsing((s, d, r) =>
                    {
                        return r.Mapper.Map<SetHeartbeatResponse>(r.Mapper.Map<SetHeartbeatResponseDto>(s));
                    });
            }
        }
    }

    public class SetHeartbeatResponseDto
    {
        public string result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<string, SetHeartbeatResponseDto>()
                    .ConvertUsing(s => new SetHeartbeatResponseDto()
                    {
                        result = s,
                    });
            }
        }
    }
}