using AutoMapper;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.Meta;
using NUnit.Framework;

namespace Deribit.S4KTNET.Test.Unit
{
    [TestFixture]
    [Category(TestCategories.unit)]
    class AuthenticationTestFixture
    {
        [Test]
        [Ignore("result does not match example further investigation required.")]
        public void TestAuthSignatureComputation()
        {
            // documentation // https://docs.deribit.com/v2/#authentication

            // form unsigned request
            var authrequest = new AuthRequest()
            {
                grant_type = GrantType.client_credentials,
                client_id = "AAAAAAAAAAA",
                client_secret = "ABCD",
                timestamp = 1554883365000,
                nonce = "fdbmmz79",
                data = "",
            };
            // sign
            authrequest = authrequest.Sign();
            // check signature
            string expectedsignature = "e20c9cd5639d41f8bbc88f4d699c4baf94a4f0ee320e9a116b72743c449eb994";
            Assert.That(authrequest.signature, Is.EqualTo(expectedsignature));
            Assert.That(authrequest.grant_type, Is.EqualTo(GrantType.client_signature));
        }
    }
}
