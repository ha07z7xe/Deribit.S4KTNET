using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Websocket
{
    //------------------------------------------------------------------------------------------------
    // documentation
    //------------------------------------------------------------------------------------------------

    // handles web socket connectivity

    //------------------------------------------------------------------------------------------------

    public interface IDeribitWebSocketService
    {
        Task Connect(CancellationToken ct);
    }

    internal class DeribitWebSocketService : IDeribitWebSocketService, IDisposable
    {
        //------------------------------------------------------------------------------------------------
        // configuration
        //------------------------------------------------------------------------------------------------

        private readonly string websocketurl_live_v2 = "wss://www.deribit.com/ws/api/v2/";
        private readonly string websocketurl_test_v2 = "wss://test.deribit.com/ws/api/v2/";

        private readonly DeribitConfig deribitconfig;

        //------------------------------------------------------------------------------------------------
        // fields
        //------------------------------------------------------------------------------------------------

        private readonly ClientWebSocket websocket;

        //------------------------------------------------------------------------------------------------
        // components
        //------------------------------------------------------------------------------------------------

        private readonly Serilog.ILogger logger;

        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly IDeribitService deribit;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitWebSocketService(IDeribitService deribit, DeribitConfig config)
        {
            this.deribit = deribit;
            this.deribitconfig = config;
            this.websocket = new ClientWebSocket();
            this.logger = Serilog.Log.ForContext<DeribitWebSocketService>();
        }

        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            this.websocket.Dispose();
        }

        //------------------------------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------------------------------
        public async Task Connect(CancellationToken ct)
        {
            // determine url
            string wssurl;
            switch (this.deribitconfig.Environment)
            {
                case DeribitEnvironment.Live:
                    wssurl = this.websocketurl_live_v2;
                    break;
                case DeribitEnvironment.Test:
                    wssurl = this.websocketurl_test_v2;
                    break;
                default:
                    throw new Exception();
            }
            // connect
            this.logger.Information($"connecting to {wssurl}");
            await this.websocket.ConnectAsync(new Uri(wssurl), ct);
        }
        //------------------------------------------------------------------------------------------------
    }
}
