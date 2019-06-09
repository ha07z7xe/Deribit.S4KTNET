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

                this.CreateMap<string, CancelOrdersResponse>()
                    .ForMember(d => d.cancelledcount, o => o.MapFrom((s, d, m) =>
                    {
                        if (int.TryParse(s, out var i))
                            return i;
                        return 0;
                    }))
                    ;
            }
        }
    }
}