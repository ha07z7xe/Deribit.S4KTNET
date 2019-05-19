using Deribit.S4KTNET.Core.Authentication;
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

        [JsonRpcMethod("public/auth")]
        Task<AuthResponseDto> auth
        (
            string grant_type,
            string username,
            string password,
            string client_id,
            string client_secret,
            string refresh_token,
            string timestamp,
            string signature,
            string nonce,
            string state,
            string scope,
            CancellationToken ct
        );

        [JsonRpcMethod("private/logout")]
        Task logout(CancellationToken ct);
    }
}
