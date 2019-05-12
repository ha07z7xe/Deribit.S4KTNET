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


        public override void Validate()
        {
            base.Validate();
            switch (this.grant_type)
            {
                case GrantType.password:
                    {
                        if (string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.password))
                            throw new Exception($"username/password not provided");
                        break;
                    }
                case GrantType.client_credentials:
                    {
                        if (string.IsNullOrEmpty(this.client_id))
                            throw new Exception($"client_id not provided");
                        if (string.IsNullOrEmpty(this.client_secret))
                            throw new Exception($"client_secret not provided");
                        break;
                    }
                case GrantType.client_signature:
                    {
                        if (string.IsNullOrEmpty(this.client_id))
                            throw new Exception($"client_id not provided");
                        if (this.timestamp == default)
                            throw new Exception("timestamp not provided");
                        break;
                    }
                case GrantType.refresh_token:
                    {
                        if (string.IsNullOrEmpty(this.refresh_token))
                            throw new Exception($"refresh_token not provided");
                        break;
                    }
            }
        }

    }

    public enum GrantType
    {
        password,
        client_credentials,
        client_signature,
        refresh_token,
    }
}