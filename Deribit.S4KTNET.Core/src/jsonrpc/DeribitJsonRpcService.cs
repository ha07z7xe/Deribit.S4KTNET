using Autofac;
using Deribit.S4KTNET.Core.WebSocket;
using StreamJsonRpc;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.JsonRpc
{
    //------------------------------------------------------------------------------------------------
    // documentation
    //------------------------------------------------------------------------------------------------

    // handles heartbeats
    // handles json rpc request execution

    //------------------------------------------------------------------------------------------------

    public interface IDeribitJsonRpcService : IDisposable
    {
        IDeribitJsonRpcProxy RpcProxy { get; }
        StreamJsonRpc.JsonRpc JsonRpc { get; }
        Task Reconnect(ReconnectionType reconnectionType, CancellationToken ct);
        event Action<ReconnectionType> ReconnectionHappened;
    }

    internal class DeribitJsonRpcService : IDeribitJsonRpcService, IDisposable
    {
        //------------------------------------------------------------------------------------------------
        // events
        //------------------------------------------------------------------------------------------------

        public event Action<ReconnectionType> ReconnectionHappened;

        //------------------------------------------------------------------------------------------------
        // configuration
        //------------------------------------------------------------------------------------------------

        private readonly DeribitConfig deribitconfig;

        private readonly int CheckConnectionLoopPeriodSecs = 30;

        //------------------------------------------------------------------------------------------------
        // loops
        //------------------------------------------------------------------------------------------------

        private System.Timers.Timer CheckConnectionLoopTimer;

        //------------------------------------------------------------------------------------------------
        // locks
        //------------------------------------------------------------------------------------------------

        private readonly object CheckConnectionLoopSyncLock = new object();

        //------------------------------------------------------------------------------------------------
        // fields
        //------------------------------------------------------------------------------------------------

        public IDeribitJsonRpcProxy RpcProxy { get; private set; }
        public StreamJsonRpc.JsonRpc JsonRpc { get; private set; }

        //------------------------------------------------------------------------------------------------
        // components
        //------------------------------------------------------------------------------------------------

        private readonly Serilog.ILogger logger;

        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly IDeribitService deribit;
        private readonly IDeribitWebSocketService wsservice;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitJsonRpcService
        (
            IDeribitService deribit, 
            IDeribitWebSocketService wsservice, 
            DeribitConfig config
        )
        {
            // dependencies
            this.deribit = deribit;
            this.deribitconfig = config;
            this.wsservice = wsservice;
            // logger
            this.logger = Serilog.Log.ForContext<DeribitJsonRpcService>();
            // message formatter
            JsonMessageFormatter messageformatter = new JsonMessageFormatter()
            {
                Encoding = Encoding.UTF8,
                ProtocolVersion = new Version(2, 0),
            };
            // web socket connection
            this.wsservice.ReconnectionHappened += this.Wsservice_ReconnectionHappened;
            // check connection loop
            {
                this.CheckConnectionLoopTimer = new System.Timers.Timer()
                {
                    Interval = TimeSpan.FromSeconds(this.CheckConnectionLoopPeriodSecs).TotalMilliseconds,
                    Enabled = false,
                };
                this.CheckConnectionLoopTimer.Elapsed += (sender, e) =>
                {
                    lock (this.CheckConnectionLoopSyncLock)
                    {
                        this.CheckConnection(default).Wait();
                    }
                };
            }
        }
        
        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitJsonRpcService>()
                    .AsSelf()
                    .As<IDeribitJsonRpcService>()
                    .SingleInstance();

                builder.Register<IDeribitJsonRpcProxy>(ctx => ctx.Resolve<IDeribitJsonRpcService>().RpcProxy);
                builder.Register<StreamJsonRpc.JsonRpc>(ctx => ctx.Resolve<IDeribitJsonRpcService>().JsonRpc);
            }
        }

        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            this.CheckConnectionLoopTimer.Stop();
            this.JsonRpc.Dispose();
        }

        //------------------------------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------------------------------

        private void Wsservice_ReconnectionHappened(ReconnectionType reconnectionType)
        {
            this.Reconnect(reconnectionType, default).Wait();
        }

        public async Task Reconnect(ReconnectionType reconnectionType, CancellationToken ct)
        {
            // attach json rpc to websocket
            WebSocketMessageHandler wsmh = new WebSocketMessageHandler(wsservice.ClientWebSocket);
            this.JsonRpc = new StreamJsonRpc.JsonRpc(wsmh);
            // build proxy // https://github.com/microsoft/vs-streamjsonrpc/blob/master/doc/dynamicproxy.md
            this.RpcProxy = this.JsonRpc.Attach<IDeribitJsonRpcProxy>(new JsonRpcProxyOptions
            {
                ServerRequiresNamedArguments = true,
            });
            // tracing
            if (this.deribitconfig.EnableJsonRpcTracing)
            {
                var listener = new global::SerilogTraceListener.SerilogTraceListener();
                this.JsonRpc.TraceSource.Listeners.Add(listener);
                this.JsonRpc.TraceSource.Switch.Level = System.Diagnostics.SourceLevels.Information;
                this.logger.Verbose("JsonRpc tracing enabled");
            }
            this.JsonRpc.StartListening();
            this.CheckConnectionLoopTimer.Start();
            this.ReconnectionHappened?.Invoke(reconnectionType);
        }

        private async Task CheckConnection(CancellationToken ct)
        {
            // ping server
            try
            {
                var testresponse = await this.RpcProxy.test("checkconnection", ct);
            }
            catch (System.Net.WebSockets.WebSocketException ex) 
            when (ex.Message.Contains("The remote party closed the WebSocket connection"))
            {
                // reconnect
                this.logger.Error(ex, $"WebSocket ping failed.");
                if (this.deribitconfig.NoAutoReconnect)
                    return;
                await this.wsservice.Reconnect(ReconnectionType.Lost, ct);
            }
            catch (StreamJsonRpc.ConnectionLostException ex)
            {
                // reconnect
                this.logger.Error(ex, $"WebSocket ping failed.");
                if (this.deribitconfig.NoAutoReconnect)
                    return;
                await this.wsservice.Reconnect(ReconnectionType.Lost, ct);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"WebSocket ping failed.");
            }
        }

        //------------------------------------------------------------------------------------------------
    }
}
