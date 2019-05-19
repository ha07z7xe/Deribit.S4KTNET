using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class DeribitConfig
    {
        public DeribitEnvironment Environment { get; set; }

        public bool ConnectOnConstruction { get; set; }

        /// <summary>
        /// log jsonrpc requests
        /// </summary>
        public bool EnableJsonRpcTracing { get; set; }

        /// <summary>
        /// Dont automatically refresh the access token periodically
        /// </summary>
        public bool NoRefreshAuthToken { get; set; }

        /// <summary>
        /// Dont automatically respond to heartbeats
        /// </summary>
        public bool NoRespondHeartbeats { get; set; }
    }
}
