using Deribit.S4KTNET.Core;
using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Deribit.S4KTNET.Core.Supporting;
using System.Threading.Tasks;
using Deribit.S4KTNET.Core.SubscriptionManagement;
using Deribit.S4KTNET.Core.SessionManagement;

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

            //// test subscription management
            await TestSubscriptionManagementApiAsync();

            // test heartbeats
            await TestHeartbeatsAsync();

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
                TestResponse testresponse = await deribit.Supporting.Test(new TestRequest()
                {
                    expected_result = null,
                });
                Log.Information($"public/test | version:{{version}}", testresponse.version);
            }

            // public/get_time
            {
                GetTimeResponse timeresponse = await deribit.Supporting.GetTime();
                Log.Information($"public/get_time | time:{{time:u}}", timeresponse.server_time);
            }

            // public/hello
            {
                HelloResponse helloresponse = await deribit.Supporting.Hello(new HelloRequest
                {
                    client_name = "clientname",
                    client_version = "1",
                });
                Log.Information($"public/hello | version:{{version}}", helloresponse.version);
            }
        }

        private static async Task TestSubscriptionManagementApiAsync()
        {
            // public/subscribe
            {
                SubscribeResponse subscriberesponse = await deribit.SubscriptionManagement
                    .SubscribePublic(new SubscribeRequest
                {
                        channels = new string[] 
                        {
                            //DeribitSubscriptions.trades(DeribitInstruments.Perpetual.BTCPERPETRUAL, Interval.raw),
                            DeribitSubscriptions.book(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval._100ms),
                        },
                });
                Log.Information($"public/subscribe | channels:{{channels}}", 
                    string.Join(',', subscriberesponse.subscribed_channels));
            }

            // wait
            await Task.Delay(5000);

            // public/unsubscribe
            {
                UnsubscribeResponse unsubscriberesponse = await deribit.SubscriptionManagement
                    .UnsubscribePublic(new UnsubscribeRequest
                    {
                        channels = new string[]
                        {
                            //DeribitSubscriptions.trades(DeribitInstruments.Perpetual.BTCPERPETRUAL, Interval.raw),
                            DeribitSubscriptions.book(DeribitInstruments.Perpetual.BTCPERPETUAL, Interval._100ms),
                        },
                    });
                Log.Information($"public/unsubscribe | channels:{{channels}}", 
                    string.Join(',', unsubscriberesponse.subscribed_channels));
            }
        }

        private static async Task TestHeartbeatsAsync()
        {
            // public/set_heartbeat
            {
                var setheartbeatresponse = await deribit.SessionManagement
                    .SetHeartbeat(new SetHeartbeatRequest()
                    {
                        interval = 10,
                    });
            }

            // wait
            await Task.Delay(13 * 1000);

            // public/disable_heartbeat
            {
                await deribit.SessionManagement.DisableHeartbeat();
            }
        }
    }
}
