using Autofac;
using Deribit.S4KTNET.Core.WebSocket;
using StreamJsonRpc;
using System;
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

    public interface IDeribitJsonRpcService
    {
        IDeribitJsonRpcProxy RpcProxy { get; }
        StreamJsonRpc.JsonRpc JsonRpc { get; }
        Task Connect(CancellationToken ct);
    }

    internal class DeribitJsonRpcService : IDeribitJsonRpcService, IDisposable
    {
        //------------------------------------------------------------------------------------------------
        // configuration
        //------------------------------------------------------------------------------------------------

        private readonly DeribitConfig deribitconfig;

        //------------------------------------------------------------------------------------------------
        // fields
        //------------------------------------------------------------------------------------------------

        public IDeribitJsonRpcProxy RpcProxy { get; }
        public StreamJsonRpc.JsonRpc JsonRpc { get; }

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
            // attach json rpc to websocket
            WebSocketMessageHandler wsmh = new WebSocketMessageHandler(wsservice.ClientWebSocket);
            this.JsonRpc = new StreamJsonRpc.JsonRpc(wsmh);
            // build proxy // https://github.com/microsoft/vs-streamjsonrpc/blob/master/doc/dynamicproxy.md
            this.RpcProxy = this.JsonRpc.Attach<IDeribitJsonRpcProxy>(new JsonRpcProxyOptions
            {
                ServerRequiresNamedArguments = true,
            });
            // tracing
            if (config.EnableJsonRpcTracing)
            {
                var listener = new global::SerilogTraceListener.SerilogTraceListener();
                this.JsonRpc.TraceSource.Listeners.Add(listener);
                this.JsonRpc.TraceSource.Switch.Level = System.Diagnostics.SourceLevels.All;
                this.logger.Verbose("JsonRpc tracing enabled");
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
            this.JsonRpc.Dispose();
        }

        //------------------------------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------------------------------

        public async Task Connect(CancellationToken ct)
        {
            this.JsonRpc.StartListening();
        }

        //------------------------------------------------------------------------------------------------
    }
}
