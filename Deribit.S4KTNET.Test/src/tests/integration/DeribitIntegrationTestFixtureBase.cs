using Deribit.S4KTNET.Core;
using Deribit.S4KTNET.Core.Authentication;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Test.Integration
{
    [TestFixture]
    [Category(TestCategories.integration)]
    abstract class DeribitIntegrationTestFixtureBase
    {
        //----------------------------------------------------------------------------
        // configuration
        //----------------------------------------------------------------------------

        protected IConfigurationRoot configurationRoot;
        protected DeribitCredentials deribitcredentials;
        protected DeribitEnvironment deribitenvironment;

        //----------------------------------------------------------------------------
        // components
        //----------------------------------------------------------------------------

        protected DeribitConfig deribitconfig;
        protected DeribitService deribit;

        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            // read config
            this.configurationRoot =
                new ConfigurationBuilder()
                    .AddJsonFile("config/config.json", false)
                    .AddJsonFile("config/secrets.json", false)
                    .Build();

            // determine environment
            if (this.configurationRoot["deribit:environment"] == "live")
                this.deribitenvironment = DeribitEnvironment.Live;
            else
                this.deribitenvironment = DeribitEnvironment.Test;

            // create config
            deribitconfig = new DeribitConfig
            {
                Environment = this.deribitenvironment,
                EnableJsonRpcTracing = true,
            };

            // bind credentials
            this.deribitcredentials = this.configurationRoot
                .GetSection("deribit:credentials")
                .Get<DeribitCredentials>();
                

            // construct services
            deribit = new DeribitService(deribitconfig);

            // connect
            await deribit.Connect(default);

            // authenticate if possible
            if (this.deribitcredentials != null)
            {
                if (this.deribitcredentials.client_id != null)
                {
                    var authresponse = await this.deribit.Authentication.Auth(new AuthRequest()
                    {
                        grant_type = GrantType.client_credentials,
                        client_id = this.deribitcredentials.client_id,
                        client_secret = this.deribitcredentials.client_secret,
                    });
                    Assert.That(authresponse.refresh_token, Is.Not.Empty);
                }
                else if (this.deribitcredentials.username != null)
                {
                    var authresponse = await this.deribit.Authentication.Auth(new AuthRequest()
                    {
                        grant_type = GrantType.password,
                        username = this.deribitcredentials.username,
                        password = this.deribitcredentials.password,
                    });
                    Assert.That(authresponse.refresh_token, Is.Not.Empty);
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            deribit.Dispose();
        }


        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        //----------------------------------------------------------------------------
    }
}
