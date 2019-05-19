using Deribit.S4KTNET.Core.Supporting;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.JsonRpc
{
    // https://github.com/microsoft/vs-streamjsonrpc/blob/master/doc/dynamicproxy.md
    partial interface IDeribitJsonRpcProxy
    {
        /*
        
        A proxy can only be dynamically generated for an interface that meets these requirements:

        No properties
        No generic methods
        All methods return Task or Task<T>
        All events are typed with EventHandler or EventHandler<T>
        Methods may accept a CancellationToken as the last parameter.

        */

        event EventHandler<JToken> heartbeat;

        [JsonRpcMethod("public/set_heartbeat")]
        Task<string> set_heartbeat(int interval, CancellationToken ct);

        [JsonRpcMethod("public/disable_heartbeat")]
        Task<string> disable_heartbeat(CancellationToken ct);
    }
}
