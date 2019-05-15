using NUnit.Framework;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Deribit.S4KTNET.Test.Integration
{
    [SetUpFixture]
    [Category(TestCategories.integration)]
    class IntegrationTestFixture
    {
        //----------------------------------------------------------------------------
        // lifecycle
        //----------------------------------------------------------------------------

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console
                (
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] " +
                    "[{SourceContext:l}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.CloseAndFlush();
        }

        //----------------------------------------------------------------------------
    }
}
