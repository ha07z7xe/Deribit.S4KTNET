using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Deribit.S4KTNET.Core.Authentication;
using Deribit.S4KTNET.Core.JsonRpc;
using Deribit.S4KTNET.Core.SessionManagement;
using Deribit.S4KTNET.Core.Supporting;
using Deribit.S4KTNET.Core.WebSocket;

namespace Deribit.S4KTNET.Core
{
    public interface IDeribitService
    {
        //------------------------------------------------------------------------------------------------
        // core services
        //------------------------------------------------------------------------------------------------

        IDeribitWebSocketService WebSocket { get; }

        IDeribitJsonRpcService JsonRpc { get; }

        //------------------------------------------------------------------------------------------------
        // sub services
        //------------------------------------------------------------------------------------------------

        IDeribitAuthenticationService Authentication { get; }

        IDeribitSessionManagementService SessionManagement { get; }

        IDeribitSupportingService Supporting { get; }

        //------------------------------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------------------------------

        Task Connect(CancellationToken ct);
        
        //------------------------------------------------------------------------------------------------
    }

    public class DeribitService : IDeribitService, IDisposable
    {
        //------------------------------------------------------------------------------------------------
        // sub services
        //------------------------------------------------------------------------------------------------

        public IDeribitWebSocketService WebSocket => this.WebSocket2;
        internal DeribitWebSocketService WebSocket2 { get; }

        public IDeribitJsonRpcService JsonRpc => this.JsonRpc2;
        internal DeribitJsonRpcService JsonRpc2 { get; }

        public IDeribitAuthenticationService Authentication => this.Authentication2;
        internal DeribitAuthenticationService Authentication2 { get; }

        public IDeribitSessionManagementService SessionManagement => this.SessionManagement2;
        internal DeribitSessionManagementService SessionManagement2 { get; }

        public IDeribitSupportingService Supporting => this.Supporting2;
        internal DeribitSupportingService Supporting2 { get; }

        //------------------------------------------------------------------------------------------------
        // configuration
        //------------------------------------------------------------------------------------------------

        internal readonly DeribitConfig deribitconfig;

        //------------------------------------------------------------------------------------------------
        // components
        //------------------------------------------------------------------------------------------------

        private readonly Serilog.ILogger logger;
        private readonly Autofac.IContainer container;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitService(DeribitConfig config)
        {
            // configuration
            this.deribitconfig = config;
            // logging
            this.logger = Serilog.Log.ForContext<DeribitService>();
            // container
            var containerbuilder = new ContainerBuilder();
            containerbuilder.RegisterModule(new DeribitModule(this));
            this.container = containerbuilder.Build();
            // core services
            this.WebSocket2 = container.Resolve<DeribitWebSocketService>();
            this.JsonRpc2 = container.Resolve<DeribitJsonRpcService>();
            // sub services
            this.Authentication2 = container.Resolve<DeribitAuthenticationService>();
            this.SessionManagement2 = container.Resolve<DeribitSessionManagementService>();
            this.Supporting2 = container.Resolve<DeribitSupportingService>();
            // connect
            if (config.ConnectOnConstruction)
            {
                this.Connect(default).Wait();
            }
        }

        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            private readonly DeribitService deribit;

            public Module(DeribitService deribit)
            {
                this.deribit = deribit;
            }

            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterInstance(deribit)
                    .AsSelf()
                    .As<IDeribitService>();

                builder.RegisterInstance(deribit.deribitconfig);
            }
        }

        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            this.container.Dispose();
        }

        //------------------------------------------------------------------------------------------------
        // connection
        //------------------------------------------------------------------------------------------------

        public async Task Connect(CancellationToken ct)
        {
            await this.WebSocket.Connect(ct);
            await this.JsonRpc.Connect(ct);
        }

        //------------------------------------------------------------------------------------------------
    }
}
