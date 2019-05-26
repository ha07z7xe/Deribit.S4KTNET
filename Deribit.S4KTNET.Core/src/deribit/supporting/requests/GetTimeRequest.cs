namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-get_time

    public class GetTimeRequest : RequestBase
    {
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetTimeRequest, GetTimeRequestDto>();
            }
        }

    }

    internal class GetTimeRequestDto
    {
        
    }
}