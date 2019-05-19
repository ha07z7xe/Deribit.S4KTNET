using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Authentication
{
    // https://docs.deribit.com/v2/#public-auth

    public class AuthResponse : ResponseBase
    {
        public string access_token { get; set; }

        /// <summary>
        /// seconds
        /// </summary>
        public int expires_in { get; set; }

        public DateTime expires_at { get; set; }

        public string refresh_token { get; set; }

        public string scope { get; set; }

        public string state { get; set; }

        public string token_type { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<AuthResponseDto, AuthResponse>()
                    .ForMember(d => d.expires_at, o => o.MapFrom(s => DateTime.UtcNow.AddSeconds(s.expires_in)))
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<AuthResponse>
        {
            public Validator()
            {
                this.RuleFor(x => x.refresh_token).NotEmpty();
                this.RuleFor(x => x.expires_at).GreaterThanOrEqualTo(DateTime.UtcNow);
                this.RuleFor(x => x.token_type).Equals("bearer");
            }
        }
    }

    public class AuthResponseDto
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string refresh_token { get; set; }

        public string scope { get; set; }

        public string state { get; set; }

        public string token_type { get; set; }
    }
}