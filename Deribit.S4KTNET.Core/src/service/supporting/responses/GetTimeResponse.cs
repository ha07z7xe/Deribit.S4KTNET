using System;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-get_time

    public class GetTimeResponse : ResponseBase
    {
        public DateTime server_time { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetTimeResponseDto, GetTimeResponse>()
                    // todo
                    .ForMember(x => x.server_time, o => o.Ignore());
            }
        }

    }

    public class GetTimeResponseDto
    {
        public long result { get; set; }
    }
}