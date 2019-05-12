namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-test

    public class TestResponse : ResponseBase
    {
        public string version { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TestResponseDto, TestResponse>();
            }
        }

    }

    public class TestResponseDto
    {
        public string version { get; set; }
    }
}