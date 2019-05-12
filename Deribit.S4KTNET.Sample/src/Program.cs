using Deribit.S4KTNET.Core;
using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Deribit.S4KTNET.Core.Supporting;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Sample
{
    class Program
    {
        static DeribitService deribit;

        static async Task Main(string[] args)
        {
            // configure serilog
            ConfigureSerilog();

            // create config
            DeribitConfig deribitconfig = new DeribitConfig
            {
                Environment = DeribitEnvironment.Test,
                EnableJsonRpcTracing = true,
            };

            // construct services
            deribit = new DeribitService(deribitconfig);

            // connect
            await deribit.Connect(default);

            // test supporting
            await TestSupportingApiAsync();

            // wait for input
            Console.ReadKey();

            // dispose
            deribit.Dispose();
        }

        private static void ConfigureSerilog()
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

        private static async Task TestSupportingApiAsync()
        {
            // public/test
            {
                TestResponse testresponse = await deribit.Supporting.test(new TestRequest()
                {
                    expected_result = null,
                });
                Log.Information($"/public/test | version:{{version}}", testresponse.version);
            }

            // public/get_time
            {
                long timems = await deribit.Supporting.get_time();
                Log.Information($"/public/get_time | time:{{time}}", timems);
            }
        }
    }
}
