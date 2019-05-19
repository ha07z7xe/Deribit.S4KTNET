using System;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    // https://docs.deribit.com/v2/#private-disable_cancel_on_disconnect

    public class DisableCancelOnDisconnectResponse : ResponseBase
    {
        public bool result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<DisableCancelOnDisconnectResponseDto, DisableCancelOnDisconnectResponse>()
                    .ForMember(x => x.result, o => o.MapFrom(x => x.result == "ok"));

                this.CreateMap<string, DisableCancelOnDisconnectResponse>()
                    .ConvertUsing((s, d, r) =>
                    {
                        return r.Mapper.Map<DisableCancelOnDisconnectResponse>(r.Mapper.Map<DisableCancelOnDisconnectResponseDto>(s));
                    });
            }
        }

    }

    public class DisableCancelOnDisconnectResponseDto
    {
        public string result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<string, DisableCancelOnDisconnectResponseDto>()
                    .ConvertUsing(s => new DisableCancelOnDisconnectResponseDto()
                    {
                        result = s,
                    });
            }
        }
    }
}