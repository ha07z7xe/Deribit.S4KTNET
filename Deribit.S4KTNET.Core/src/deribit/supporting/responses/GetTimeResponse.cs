using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-get_time

    public class GetTimeResponse : ResponseBase
    {
        public DateTime server_time { get; set; }

        public long server_time_millis { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<long, GetTimeResponse>()
                    .ForMember(x => x.server_time_millis, o => o.MapFrom(x => x))
                    .ForMember(x => x.server_time, 
                        o => o.ConvertUsing<UnixTimestampMillisValueResolver, long>(x => x));
            }
        }

    }
}