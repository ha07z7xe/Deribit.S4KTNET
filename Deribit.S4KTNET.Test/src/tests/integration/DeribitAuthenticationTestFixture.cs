using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Authentication;
using NUnit.Framework;
using StreamJsonRpc;
using System;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    class DeribitAuthenticationTestFixture : DeribitIntegrationTestFixtureBase
    {
        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            if (this.deribitcredentials == null)
                throw new Exception("This test requires credentials");
        }

        //----------------------------------------------------------------------------
        // public/auth
        //----------------------------------------------------------------------------

        [Test]
        [Description("public/auth (password)")]
        [Order(1)]
        public async Task Test_auth_password_success()
        {
            if (this.deribitcredentials.username == null || this.deribitcredentials.password == null)
            {
                throw new Exception("This test requires username/password");
            }
            AuthResponse authresponse = await deribit.Authentication.Auth(new AuthRequest()
            {
                grant_type = GrantType.password,
                username = this.deribitcredentials.username,
                password = this.deribitcredentials.password,
            });
            Assert.That(authresponse.refresh_token, Is.Not.Null);
        }

        [Test]
        [Description("public/auth (password)")]
        [Ignore("Breaks the websocket connection")]
        public async Task Test_auth_password_badcredentials()
        {
            Assert.ThrowsAsync<RemoteInvocationException>(async () =>
            {
                AuthResponse authresponse = await deribit.Authentication.Auth(new AuthRequest()
                {
                    grant_type = GrantType.password,
                    username = "badusername",
                    password = "badpassword",
                });
            });
        }

        [Test]
        [Description("public/auth (client credentials)")]
        [Order(2)]
        public async Task Test_auth_clientcredentials_success()
        {
            if (this.deribitcredentials.client_id == null || this.deribitcredentials.client_secret == null)
            {
                throw new Exception("This test requires client credentials");
            }
            AuthResponse authresponse = await deribit.Authentication.Auth(new AuthRequest()
            {
                grant_type = GrantType.client_credentials,
                client_id = this.deribitcredentials.client_id,
                client_secret = this.deribitcredentials.client_secret,
            });
            Assert.That(authresponse.refresh_token, Is.Not.Null);
        }

        [Test]
        [Description("public/auth (signed)")]
        [Order(3)]
        [Ignore("Client signature computation is not correct")]
        public async Task Test_auth_clientsignature_success()
        {
            if (this.deribitcredentials.client_id == null || this.deribitcredentials.client_secret == null)
            {
                throw new Exception("This test requires client credentials");
            }
            AuthResponse authresponse = await deribit.Authentication.Auth(new AuthRequest()
            {
                grant_type = GrantType.client_credentials,
                client_id = this.deribitcredentials.client_id,
                client_secret = this.deribitcredentials.client_secret,
            }.Sign());
            Assert.That(authresponse.refresh_token, Is.Not.Null);
        }

        //----------------------------------------------------------------------------
        // private/logout
        //----------------------------------------------------------------------------

        [Test]
        [Description("private/logout")]
        [Order(100)]
        public async Task Test_auth_logout_success()
        {
            await deribit.Authentication.Logout();
        }
        
        //----------------------------------------------------------------------------
    }
}
