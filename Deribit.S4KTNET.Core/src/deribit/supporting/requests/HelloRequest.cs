using FluentValidation;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-hello

    public class HelloRequest : RequestBase
    {
        public string client_name { get; set; }

        public string client_version { get; set; }

        internal class Validator : AbstractValidator<HelloRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.client_name).NotEmpty();
                this.RuleFor(x => x.client_version).NotEmpty();
            }
        }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<HelloRequest, HelloRequestDto>();
            }
        }
    }

    internal class HelloRequestDto
    {
        public string client_name { get; set; }

        public string client_version { get; set; }
    }
}