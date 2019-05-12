using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class DeribitConfig
    {
        public DeribitEnvironment Environment { get; set; }

        public bool ConnectOnConstruction { get; set; }

        public bool EnableJsonRpcTracing { get; set; }
    }
}
