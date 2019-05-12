using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Authentication
{
    // https://docs.deribit.com/v2/#public-auth

    public class AuthRequest : RequestBase
    {
        public GrantType grant_type { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string client_id { get; set; }

        public string client_secret { get; set; }

        public string refresh_token { get; set; }

        public DateTime timestamp { get; set; }

        public string signature { get; set; }

        public string nonce { get; set; }

        public string state { get; set; }

        public string scope { get; set; }

        internal class Validator : AbstractValidator<AuthRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.username).NotEmpty().When(x => x.grant_type == GrantType.password);
                this.RuleFor(x => x.password).NotEmpty().When(x => x.grant_type == GrantType.password);
                this.RuleFor(x => x.client_id).NotEmpty().When(x => x.grant_type == GrantType.client_credentials);
                this.RuleFor(x => x.client_id).NotEmpty().When(x => x.grant_type == GrantType.client_signature);
                this.RuleFor(x => x.client_secret).NotEmpty().When(x => x.grant_type == GrantType.client_credentials);
                this.RuleFor(x => x.refresh_token).NotEmpty().When(x => x.grant_type == GrantType.refresh_token);
                this.RuleFor(x => x.timestamp).NotEmpty().When(x => x.grant_type == GrantType.client_signature);
                this.RuleFor(x => x.signature).NotEmpty().When(x => x.grant_type == GrantType.client_signature);
                // this.RuleFor(x => x.nonce).NotEmpty().When(x => x.grant_type == GrantType.client_signature);
            }
        }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<AuthRequest, AuthRequestDto>()
                    .ForMember(x => x.grant_type, o => o.MapFrom(x => x.grant_type.ToString()))
                    // is this unix seconds ? not sure
                    .ForMember(x => x.timestamp, o => o.Ignore());
            }
        }
    }

    public class AuthRequestDto
    {
        public string grant_type { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string client_id { get; set; }

        public string client_secret { get; set; }

        public string refresh_token { get; set; }

        public string timestamp { get; set; }

        public string signature { get; set; }

        public string nonce { get; set; }

        public string state { get; set; }

        public string scope { get; set; }
    }

    public enum GrantType
    {
        password,
        client_credentials,
        client_signature,
        refresh_token,
    }

    public static class AccessScopes
    {
        public const string connection = "connection";
        public static string session() => "session";
        public static string session(string name) => $"session:{name}";
        public static class trade
        {
            public const string read = "trade:read";
            public const string read_write = "trade:read_write";
            public const string none = "trade:none";
        }
        public static class wallet
        {
            public const string read = "wallet:read";
            public const string read_write = "wallet:read_write";
            public const string none = "wallet:none";
        }
        public static class account
        {
            public const string read = "account:read";
            public const string read_write = "account:read_write";
            public const string none = "account:none";
        }
        public static string expires(int n) => $"expires:{n}";
        public static string ip(string addr) => $"ip:{addr}";
    }
}