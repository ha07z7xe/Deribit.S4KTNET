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

        [JsonRpcMethod("public/get_time")]
        Task<long> get_time(CancellationToken ct);

        [JsonRpcMethod("public/hello")]
        Task<HelloResponseDto> hello(string client_name, string client_version, CancellationToken ct);

        [JsonRpcMethod("public/test")]
        Task<TestResponseDto> test(string expected_result, CancellationToken ct);
    }
}
