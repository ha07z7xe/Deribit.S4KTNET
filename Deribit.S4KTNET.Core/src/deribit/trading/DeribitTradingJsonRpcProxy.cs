using Deribit.S4KTNET.Core.Supporting;
using Deribit.S4KTNET.Core.Trading;
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

        [JsonRpcMethod("private/buy")]
        Task<BuySellResponseDto> buy
        (
            string instrument_name,
            decimal amount,
            string type,
            string label,
            decimal? price,
            string time_in_force,
            decimal? max_show,
            bool post_only,
            bool reduce_only,
            decimal? stop_price,
            string trigger,
            string advanced,
            CancellationToken ct
        );

        [JsonRpcMethod("private/sell")]
        Task<BuySellResponseDto> sell
        (
            string instrument_name,
            decimal amount,
            string type,
            string label,
            decimal? price,
            string time_in_force,
            decimal? max_show,
            bool post_only,
            bool reduce_only,
            decimal? stop_price,
            string trigger,
            string advanced,
            CancellationToken ct
        );

        [JsonRpcMethod("private/cancel")]
        Task<OrderDto> cancel(string order_id, CancellationToken ct);

        [JsonRpcMethod("private/cancel_all")]
        Task<string> cancel_all(CancellationToken ct);

    }
}
