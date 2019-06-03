namespace Deribit.S4KTNET.Core.Trading
{
    public class CancelOrdersResponse
    {
        public int cancelledcount { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<int, CancelOrdersResponse>()
                    .ForMember(d => d.cancelledcount, o => o.MapFrom(s => s))
                    ;
            }
        }
    }
}