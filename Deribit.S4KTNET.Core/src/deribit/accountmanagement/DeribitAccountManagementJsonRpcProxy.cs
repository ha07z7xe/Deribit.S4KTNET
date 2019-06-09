using Deribit.S4KTNET.Core.AccountManagement;
using StreamJsonRpc;
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

        [JsonRpcMethod("private/get_account_summary")]
        Task<AccountDto> get_account_summary(string currency, bool? extended, CancellationToken ct);

        [JsonRpcMethod("private/get_position")]
        Task<PositionDto> get_position(string instrument_name, CancellationToken ct);

        [JsonRpcMethod("private/get_positions")]
        Task<PositionDto[]> get_positions(string currency, string kind, CancellationToken ct);

        [JsonRpcMethod("private/get_subaccounts")]
        Task<AccountDto[]> get_subaccounts(bool? with_portfolio, CancellationToken ct);
    }
}
