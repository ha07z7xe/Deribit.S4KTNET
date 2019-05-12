using Deribit.S4KTNET.Core;
using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Deribit.S4KTNET.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // configure serilog
            ConfigureSerilog();

            // create config
            DeribitConfig deribitconfig = new DeribitConfig
            {
                Environment = DeribitEnvironment.Test,
            };

            // construct services
            DeribitService deribit = new DeribitService(deribitconfig);

            // connect
            deribit.Connect(default).Wait();

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
    }
}
