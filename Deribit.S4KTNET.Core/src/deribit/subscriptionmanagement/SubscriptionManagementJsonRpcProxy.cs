using Deribit.S4KTNET.Core.SubscriptionManagement;
using Deribit.S4KTNET.Core.Supporting;
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

        event EventHandler<SubscriptionNotificationDto> subscription;

        [JsonRpcMethod("public/subscribe")]
        Task<string[]> subscribe_public(string[] channels, CancellationToken ct);

        [JsonRpcMethod("public/unsubscribe")]
        Task<string[]> unsubscribe_public(string[] channels, CancellationToken ct);

        [JsonRpcMethod("private/subscribe")]
        Task<string[]> subscribe_private(string[] channels, CancellationToken ct);

        [JsonRpcMethod("private/unsubscribe")]
        Task<string[]> unsubscribe_private(string[] channels, CancellationToken ct);
    }
}
