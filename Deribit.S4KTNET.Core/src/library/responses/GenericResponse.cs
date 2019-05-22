namespace Deribit.S4KTNET.Core
{
    public class GenericResponse
    {
        public string message { get; set; }

        public bool success { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<string, GenericResponse>()
                    .ForMember(d => d.message, o => o.MapFrom(s => s))
                    .ForMember(d => d.success, o => o.MapFrom(s => s == "ok"))
                    ;
            }
        }
    }
}