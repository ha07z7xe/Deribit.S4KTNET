using System;
using System.Threading;
using System.Threading.Tasks;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.JsonRpc;
using Deribit.S4KTNET.Core.SessionManagement;
using Deribit.S4KTNET.Core.Supporting;
using Deribit.S4KTNET.Core.Websocket;

namespace Deribit.S4KTNET.Core
{
    public interface IDeribitService
    {
        IDeribitWebSocketService WebSocket { get; }

        IDeribitJsonRpcService JsonRpc { get; }

        IDeribitAuthenticationService Authentication { get; }

        IDeribitSessionManagementService SessionManagement { get; }

        IDeribitSupportingService Supporting { get; }

        //------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------

        Task Connect(CancellationToken ct);
        
        //------------------------------------------------------------------------
    }

    public class DeribitService : IDeribitService, IDisposable
    {
        //------------------------------------------------------------------------
        // sub services
        //------------------------------------------------------------------------

        public IDeribitWebSocketService WebSocket { get; }

        public IDeribitJsonRpcService JsonRpc { get; }

        public IDeribitAuthenticationService Authentication { get; }

        public IDeribitSessionManagementService SessionManagement { get; }

        public IDeribitSupportingService Supporting { get; }

        //------------------------------------------------------------------------
        // configuration
        //------------------------------------------------------------------------

        private readonly DeribitConfig deribitconfig;

        //------------------------------------------------------------------------
        // components
        //------------------------------------------------------------------------

        private readonly Serilog.ILogger logger;

        //------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------

        public DeribitService(DeribitConfig config)
        {
            this.deribitconfig = config;
            this.logger = Serilog.Log.ForContext<DeribitService>();
            this.WebSocket = new DeribitWebSocketService(this, config);
            this.JsonRpc = new DeribitJsonRpcService(this, WebSocket, config);
            this.Authentication = new DeribitAuthenticationService(this);
            this.SessionManagement = new DeribitSessionManagementService(this);
            this.Supporting = new DeribitSupportingService(this);
            if (config.ConnectOnConstruction)
            {
                this.Connect(default).Wait();
            }
        }

        //------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------

        public void Dispose()
        {
            (this.WebSocket as DeribitWebSocketService).Dispose();
            (this.JsonRpc as DeribitJsonRpcService).Dispose();
        }

        //------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------

        public async Task Connect(CancellationToken ct)
        {
            await this.WebSocket.Connect(ct);
        }

        //------------------------------------------------------------------------

        //------------------------------------------------------------------------
    }
}
