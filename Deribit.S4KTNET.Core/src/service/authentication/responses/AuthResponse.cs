using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Authentication
{
    // https://docs.deribit.com/v2/#public-auth

    public class AuthResponse : ResponseBase
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string refresh_token { get; set; }

        public string scope { get; set; }

        public string state { get; set; }

        public string token_type { get; set; }
    }
}