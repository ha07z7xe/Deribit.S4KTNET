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

        [JsonRpcMethod("private/edit")]
        Task<EditOrderResponseDto> edit
        (
            string order_id,
            decimal amount,
            decimal price,
            bool post_only,
            decimal? stop_price,
            string advanced,
            CancellationToken ct
        );

        [JsonRpcMethod("private/cancel")]
        Task<OrderDto> cancel(string order_id, CancellationToken ct);

        [JsonRpcMethod("private/cancel_all")]
        Task<string> cancel_all(CancellationToken ct);

        [JsonRpcMethod("private/cancel_all_by_currency")]
        Task<string> cancel_all_by_currency
        (
            string currency,
            string kind,
            string type,
            CancellationToken ct
        );

        [JsonRpcMethod("private/cancel_all_by_instrument")]
        Task<string> cancel_all_by_instrument
        (
            string instrument_name,
            string type,
            CancellationToken ct
        );

        [JsonRpcMethod("private/close_position")]
        Task<ClosePositionResponseDto> close_position
        (
            string instrument_name,
            string type,
            decimal? price,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_margins")]
        Task<GetMarginsResponseDto> get_margins
        (
            string instrument_name,
            decimal amount,
            decimal price,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_open_orders_by_currency")]
        Task<OrderDto[]> get_open_orders_by_currency
        (
            string currency,
            string kind,
            string type,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_open_orders_by_instrument")]
        Task<OrderDto[]> get_open_orders_by_instrument
        (
            string instrument_name,
            string type,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_order_history_by_instrument")]
        Task<OrderDto[]> get_order_history_by_instrument
        (
            string instrument_name,
            int? count,
            int? offset,
            bool? include_old,
            bool? include_unified,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_order_state")]
        Task<OrderDto> get_order_state
        (
            string order_id,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_user_trades_by_instrument")]
        Task<GetUserTradesByInstrumentResponseDto> get_user_trades_by_instrument
        (
            string instrument_name,
            int? start_seq,
            int? end_seq,
            int? count,
            bool? include_old,
            string sorting,
            CancellationToken ct
        );

        [JsonRpcMethod("private/get_user_trades_by_order")]
        Task<TradeDto[]> get_user_trades_by_order
        (
            string order_id,
            string sorting,
            CancellationToken ct
        );
    }
}
