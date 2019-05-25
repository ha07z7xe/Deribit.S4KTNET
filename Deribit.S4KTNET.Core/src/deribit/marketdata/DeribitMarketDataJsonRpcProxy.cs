using Deribit.S4KTNET.Core.MarketData;
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

        [JsonRpcMethod("public/get_book_summary_by_instrument")]
        Task<BookSummaryDto[]> get_book_summary_by_instrument(string instrument_name, CancellationToken ct);

        [JsonRpcMethod("public/get_contract_size")]
        Task<GetContractSizeResponseDto> get_contract_size(string instrument_name, CancellationToken ct);

        [JsonRpcMethod("public/get_currencies")]
        Task<CurrencyDto[]> get_currencies(CancellationToken ct);

        [JsonRpcMethod("public/get_index")]
        Task<GetIndexResponseDto> get_index(string currency, CancellationToken ct);

        [JsonRpcMethod("public/get_instruments")]
        Task<InstrumentDto[]> get_instruments(string currency, string kind, bool expired, CancellationToken ct);

        [JsonRpcMethod("public/get_last_trades_by_instrument")]
        Task<GetLastTradesByInstrumentResponseDto> get_last_trades_by_instrument
        (
            string instrument_name,
            int? start_seq,
            int? end_seq,
            int? count,
            bool? include_old,
            string sorting,
            CancellationToken ct
        );

        [JsonRpcMethod("public/get_order_book")]
        Task<OrderBookDto> get_order_book(string instrument_name, int? depth, CancellationToken ct);
    }
}
