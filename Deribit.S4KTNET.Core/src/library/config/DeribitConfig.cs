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
        /// Automatically refresh the access token periodically
        /// </summary>
        public bool RefreshAuthToken { get; set; }
    }
}
