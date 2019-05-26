using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Security.Cryptography;
using System.Text;

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

        public long timestamp { get; set; }

        public string signature { get; set; }

        public string nonce { get; set; }

        public string state { get; set; }

        public string scope { get; set; }

        public string data { get; set; }

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
                    .ForMember(d => d.grant_type, o => o.MapFrom(s => s.grant_type.ToString()))
                    .ForMember(d => d.timestamp, o => o.MapFrom(s => s.timestamp.ToString()));
            }
        }

        // https://docs.deribit.com/v2/#authentication
        /// <summary>
        /// Sign the request
        public AuthRequest Sign()
        {
            // validate
            if (this.grant_type != GrantType.client_credentials)
            {
                throw new InvalidOperationException($"GrantType must be {GrantType.client_credentials} prior to signing.");
            }
            if (this.client_id == null || this.client_secret == null)
            {
                throw new InvalidOperationException($"ClientCredentials are not set.");
            }
            byte[] clientsecretbytes = Convert.FromBase64String(this.client_secret);
            // locals
            data = data ?? "";
            Random random = new Random();
            var hmacsha256 = new HMACSHA256(clientsecretbytes);
            // determine timestamp
            this.timestamp = this.timestamp != default ? this.timestamp : DateTime.UtcNow.UnixTimeStampDateTimeUtcToMillis();
            // determine nonce
            this.nonce = this.nonce != default ? this.nonce : random.Next(int.MaxValue).ToString();
            // determine string to sign
            string stringtosign = $"{timestamp}\n{nonce}\n{data}";
            byte[] stringtosignbytes = Encoding.UTF8.GetBytes(stringtosign);
            // sign
            byte[] signaturebytes = hmacsha256.ComputeHash(stringtosignbytes);
            this.signature = signaturebytes.ByteArrayToHexString()
                .ToLowerInvariant();
            this.grant_type = GrantType.client_signature;
            // dispose resources
            hmacsha256.Dispose();
            // return
            return this;
        }
    }

    internal class AuthRequestDto
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

        public string data { get; set; }
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