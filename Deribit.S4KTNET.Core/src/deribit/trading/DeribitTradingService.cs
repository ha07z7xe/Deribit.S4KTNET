using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Trading
{
    public interface IDeribitTradingService
    {
        Task<BuySellResponse> Buy(BuySellRequest request, CancellationToken ct = default);

        Task<BuySellResponse> Sell(BuySellRequest request, CancellationToken ct = default);

        Task<EditOrderResponse> EditOrder(EditOrderRequest request, CancellationToken ct = default);

        Task<Order> Cancel(CancelRequest request, CancellationToken ct = default);

        Task<CancelOrdersResponse> CancelAll(CancellationToken ct = default);

        Task<CancelOrdersResponse> CancelAllByCurrency(CancelAllByCurrencyRequest request, CancellationToken ct = default);

        Task<CancelOrdersResponse> CancelAllByInstrument(CancelAllByInstrumentRequest request, CancellationToken ct = default);

        Task<ClosePositionResponse> ClosePosition(ClosePositionRequest request, CancellationToken ct = default);

        Task<GetMarginsResponse> GetMargins(GetMarginsRequest request, CancellationToken ct = default);

        Task<IList<Order>> GetOpenOrdersByCurrency(GetOpenOrdersByCurrencyRequest request, CancellationToken ct = default);

        Task<IList<Order>> GetOpenOrdersByInstrument(GetOpenOrdersByInstrumentRequest request, CancellationToken ct = default);

        Task<IList<Order>> GetOrderHistoryByCurrency(GetOrderHistoryByCurrencyRequest request, CancellationToken ct = default);

        Task<IList<Order>> GetOrderHistoryByInstrument(GetOrderHistoryByInstrumentRequest request, CancellationToken ct = default);

        Task<Order> GetOrderState(GetOrderStateRequest request, CancellationToken ct = default);

        Task<GetUserTradesByInstrumentResponse> GetUserTradesByInstrument(GetUserTradesByInstrumentRequest request, CancellationToken ct = default);

        Task<IList<Trade>> GetUserTradesByOrder(GetUserTradesByOrderRequest request, CancellationToken ct = default);
    }

    internal class DeribitTradingService : IDeribitTradingService
    {
        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcService jsonrpc;

        //------------------------------------------------------------------------------------------------
        // fields
        //------------------------------------------------------------------------------------------------

        private DeribitEnvironment environment => this.deribit.deribitconfig.Environment;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitTradingService(DeribitService deribit, IMapper mapper,
            IDeribitJsonRpcService jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.jsonrpc = jsonrpc;
        }
        
        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitTradingService>()
                    .AsSelf()
                    .As<IDeribitTradingService>()
                    .SingleInstance();
            }
        }


        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------

        public async Task<BuySellResponse> Buy(BuySellRequest request, CancellationToken ct)
        {
            // validate
            new BuySellRequest.Validator().ValidateAndThrow(request);
            // map request
            BuySellRequestDto reqdto = mapper.Map<BuySellRequestDto>(request);
            // execute request
            BuySellResponseDto responsedto;
            try
            {
                responsedto = await this.jsonrpc.RpcProxy.buy
                (
                    instrument_name: reqdto.instrument_name,
                    amount: reqdto.amount,
                    type: reqdto.type,
                    label: reqdto.label,
                    price: reqdto.price,
                    time_in_force: reqdto.time_in_force,
                    max_show: reqdto.max_show,
                    post_only: reqdto.post_only,
                    reduce_only: reqdto.reduce_only,
                    stop_price: reqdto.stop_price,
                    trigger: reqdto.trigger,
                    advanced: reqdto.advanced,
                    ct: ct
                );
            }
            catch (RemoteInvocationException rie) when (rie.Message.Contains("other_reject"))
            {
                return new BuySellResponse()
                {
                    rejected = true,
                    message = rie.Message,
                };
            }
            catch (RemoteInvocationException rie) when (rie.Message.Contains("not_enough_funds"))
            {
                return new BuySellResponse()
                {
                    rejected = true,
                    message = rie.Message,
                };
            }
            catch (RemoteInvocationException rie) when (rie.Message.Contains("max_open_orders_per_instrument"))
            {
                return new BuySellResponse()
                {
                    rejected = true,
                    message = rie.Message,
                };
            }
            // map response
            BuySellResponse response = mapper.Map<BuySellResponse>(responsedto);
            // validate
            new BuySellResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<BuySellResponse> Sell(BuySellRequest request, CancellationToken ct)
        {
            // validate
            new BuySellRequest.Validator().ValidateAndThrow(request);
            // map request
            BuySellRequestDto reqdto = mapper.Map<BuySellRequestDto>(request);
            // execute request
            BuySellResponseDto responsedto;
            try
            {
                responsedto = await this.jsonrpc.RpcProxy.sell
                (
                    instrument_name: reqdto.instrument_name,
                    amount: reqdto.amount,
                    type: reqdto.type,
                    label: reqdto.label,
                    price: reqdto.price,
                    time_in_force: reqdto.time_in_force,
                    max_show: reqdto.max_show,
                    post_only: reqdto.post_only,
                    reduce_only: reqdto.reduce_only,
                    stop_price: reqdto.stop_price,
                    trigger: reqdto.trigger,
                    advanced: reqdto.advanced,
                    ct: ct
                );
            }
            catch (RemoteInvocationException rie) when (rie.Message.Contains("other_reject"))
            {
                return new BuySellResponse()
                {
                    rejected = true,
                    message = rie.Message,
                };
            }
            catch (RemoteInvocationException rie) when (rie.Message.Contains("not_enough_funds"))
            {
                return new BuySellResponse()
                {
                    rejected = true,
                    message = rie.Message,
                };
            }
            catch (RemoteInvocationException rie) when (rie.Message.Contains("max_open_orders_per_instrument"))
            {
                return new BuySellResponse()
                {
                    rejected = true,
                    message = rie.Message,
                };
            }
            // map response
            BuySellResponse response = mapper.Map<BuySellResponse>(responsedto);
            // validate
            new BuySellResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<EditOrderResponse> EditOrder(EditOrderRequest request, CancellationToken ct = default)
        {
            // validate
            new EditOrderRequest.Validator().ValidateAndThrow(request);
            // map request
            EditOrderRequestDto reqdto = mapper.Map<EditOrderRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.edit
            (
                order_id: reqdto.order_id,
                amount: reqdto.amount,
                price: reqdto.price,
                post_only: reqdto.post_only,
                stop_price: reqdto.stop_price,
                advanced: reqdto.advanced,
                ct: ct
            );
            // map response
            EditOrderResponse response = mapper.Map<EditOrderResponse>(responsedto);
            // validate
            new EditOrderResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<Order> Cancel(CancelRequest request, CancellationToken ct)
        {
            // validate 
            new CancelRequest.Validator().ValidateAndThrow(request);
            // map request
            CancelRequestDto reqdto = mapper.Map<CancelRequestDto>(request);
            // execute request
            var orderdto = await this.jsonrpc.RpcProxy.cancel(reqdto.order_id, ct);
            // map response
            Order order = mapper.Map<Order>(orderdto);
            // return
            return order;
        }

        public async Task<CancelOrdersResponse> CancelAll(CancellationToken ct)
        {
            if (this.deribit.deribitconfig.Environment == DeribitEnvironment.Test)
            {
                // execute request
                var responsedto = await this.jsonrpc.RpcProxy.cancel_all_testnet(ct);
                // map response
                CancelOrdersResponse response = this.mapper.Map<CancelOrdersResponse>(responsedto);
                // return
                return response;
            }
            else if (this.deribit.deribitconfig.Environment == DeribitEnvironment.Live)
            {
                // execute request
                var responsedto = await this.jsonrpc.RpcProxy.cancel_all_livenet(ct);
                // map response
                CancelOrdersResponse response = this.mapper.Map<CancelOrdersResponse>(responsedto);
                // return
                return response;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<CancelOrdersResponse> CancelAllByCurrency(CancelAllByCurrencyRequest request, CancellationToken ct)
        {
            if (this.environment == DeribitEnvironment.Test)
            {
                // validate request
                new CancelAllByCurrencyRequest.Validator().ValidateAndThrow(request);
                // map request
                var reqdto = this.mapper.Map<CancelAllByCurrencyRequestDto>(request);
                // validate request dto
                new CancelAllByCurrencyRequestDto.Validator().ValidateAndThrow(reqdto);
                // execute request
                var responsedto = await this.jsonrpc.RpcProxy.cancel_all_by_currency_testnet
                (
                    currency: reqdto.currency,
                    kind: reqdto.kind,
                    type: reqdto.type,
                    ct
                );
                // map response
                CancelOrdersResponse response = this.mapper.Map<CancelOrdersResponse>(responsedto);
                // return
                return response;
            }
            else if (this.environment == DeribitEnvironment.Live)
            {
                // validate request
                new CancelAllByCurrencyRequest.Validator().ValidateAndThrow(request);
                // map request
                var reqdto = this.mapper.Map<CancelAllByCurrencyRequestDto>(request);
                // validate request dto
                new CancelAllByCurrencyRequestDto.Validator().ValidateAndThrow(reqdto);
                // execute request
                var responsedto = await this.jsonrpc.RpcProxy.cancel_all_by_currency_livenet
                (
                    currency: reqdto.currency,
                    kind: reqdto.kind,
                    type: reqdto.type,
                    ct
                );
                // map response
                CancelOrdersResponse response = this.mapper.Map<CancelOrdersResponse>(responsedto);
                // return
                return response;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<CancelOrdersResponse> CancelAllByInstrument(CancelAllByInstrumentRequest request, CancellationToken ct)
        {
            if (this.environment == DeribitEnvironment.Test)
            {
                // validate request
                new CancelAllByInstrumentRequest.Validator().ValidateAndThrow(request);
                // map request
                var reqdto = this.mapper.Map<CancelAllByInstrumentRequestDto>(request);
                // validate request dto
                new CancelAllByInstrumentRequestDto.Validator().ValidateAndThrow(reqdto);
                // execute request
                var responsedto = await this.jsonrpc.RpcProxy.cancel_all_by_instrument_testnet
                (
                    instrument_name: reqdto.instrument_name,
                    type: reqdto.type,
                    ct
                );
                // map response
                CancelOrdersResponse response = this.mapper.Map<CancelOrdersResponse>(responsedto);
                // return
                return response;
            }
            else if (this.environment == DeribitEnvironment.Live)
            {
                // validate request
                new CancelAllByInstrumentRequest.Validator().ValidateAndThrow(request);
                // map request
                var reqdto = this.mapper.Map<CancelAllByInstrumentRequestDto>(request);
                // validate request dto
                new CancelAllByInstrumentRequestDto.Validator().ValidateAndThrow(reqdto);
                // execute request
                var responsedto = await this.jsonrpc.RpcProxy.cancel_all_by_instrument_livenet
                (
                    instrument_name: reqdto.instrument_name,
                    type: reqdto.type,
                    ct
                );
                // map response
                CancelOrdersResponse response = this.mapper.Map<CancelOrdersResponse>(responsedto);
                // return
                return response;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<ClosePositionResponse> ClosePosition(ClosePositionRequest request, CancellationToken ct)
        {
            // validate request
            new ClosePositionRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<ClosePositionRequestDto>(request);
            // validate request dto
            new ClosePositionRequestDto.Validator().ValidateAndThrow(reqdto);
            // execute request
            ClosePositionResponseDto responsedto;
            try
            {
                responsedto = await this.jsonrpc.RpcProxy.close_position
                (
                    instrument_name: reqdto.instrument_name,
                    type: reqdto.type,
                    price: reqdto.price,
                    ct
                );
            }
            catch (RemoteInvocationException rie) when 
            (
                rie.Message.Contains("already_closed") // test
                //|| rie.Message == "internal_server_error"  // live
            )
            {
                return new ClosePositionResponse()
                {
                    already_closed = true,
                };
            }
            // map response
            ClosePositionResponse response = this.mapper.Map<ClosePositionResponse>(responsedto);
            // validate response
            new ClosePositionResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<GetMarginsResponse> GetMargins(GetMarginsRequest request, CancellationToken ct)
        {
            // validate request
            new GetMarginsRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetMarginsRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_margins
            (
                instrument_name: reqdto.instrument_name,
                amount: reqdto.amount,
                price: reqdto.price,
                ct
            );
            // map response
            GetMarginsResponse response = this.mapper.Map<GetMarginsResponse>(responsedto);
            // validate response
            new GetMarginsResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<IList<Order>> GetOpenOrdersByCurrency(GetOpenOrdersByCurrencyRequest request, CancellationToken ct)
        {
            // validate request
            new GetOpenOrdersByCurrencyRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOpenOrdersByCurrencyRequestDto>(request);
            // validate request
            new GetOpenOrdersByCurrencyRequestDto.Validator().ValidateAndThrow(reqdto);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_open_orders_by_currency
            (
                currency: reqdto.currency,
                kind: reqdto.kind,
                type: reqdto.type,
                ct
            );
            // map response
            IList<Order> response = this.mapper.Map<IList<Order>>(responsedto);
            // validate response
            foreach (var order in response)
            {
                new Order.Validator().ValidateAndThrow(order);
            }
            // return
            return response;
        }

        public async Task<IList<Order>> GetOpenOrdersByInstrument(GetOpenOrdersByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new GetOpenOrdersByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOpenOrdersByInstrumentRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_open_orders_by_instrument
            (
                instrument_name: reqdto.instrument_name,
                type: reqdto.type,
                ct
            );
            // map response
            IList<Order> response = this.mapper.Map<IList<Order>>(responsedto);
            // validate response
            foreach (var order in response)
            {
                new Order.Validator().ValidateAndThrow(order);
            }
            // return
            return response;
        }

        public async Task<IList<Order>> GetOrderHistoryByCurrency(GetOrderHistoryByCurrencyRequest request, CancellationToken ct)
        {
            // validate request
            new GetOrderHistoryByCurrencyRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOrderHistoryByCurrencyRequestDto>(request);
            // validate request
            new GetOrderHistoryByCurrencyRequestDto.Validator().ValidateAndThrow(reqdto);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_order_history_by_currency
            (
                currency: reqdto.currency,
                kind: reqdto.kind,
                count: reqdto.count,
                offset: reqdto.offset,
                include_old: reqdto.include_old,
                include_unified: reqdto.include_unified,
                ct
            );
            // map response
            IList<Order> response = this.mapper.Map<IList<Order>>(responsedto);
            // validate response
            foreach (var order in response)
            {
                new Order.Validator().ValidateAndThrow(order);
            }
            // return
            return response;
        }

        public async Task<IList<Order>> GetOrderHistoryByInstrument(GetOrderHistoryByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new GetOrderHistoryByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOrderHistoryByInstrumentRequestDto>(request);
            // validate request
            new GetOrderHistoryByInstrumentRequestDto.Validator().ValidateAndThrow(reqdto);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_order_history_by_instrument
            (
                instrument_name: reqdto.instrument_name,
                count: reqdto.count,
                offset: reqdto.offset,
                include_old: reqdto.include_old,
                include_unified: reqdto.include_unified,
                ct
            );
            // map response
            IList<Order> response = this.mapper.Map<IList<Order>>(responsedto);
            // validate response
            foreach (var order in response)
            {
                new Order.Validator().ValidateAndThrow(order);
            }
            // return
            return response;
        }

        public async Task<Order> GetOrderState(GetOrderStateRequest request, CancellationToken ct)
        {
            // validate request
            new GetOrderStateRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOrderStateRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_order_state
            (
                order_id: reqdto.order_id,
                ct
            );
            // map response
            Order response = this.mapper.Map<Order>(responsedto);
            // validate response
            new Order.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<GetUserTradesByInstrumentResponse> GetUserTradesByInstrument(GetUserTradesByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new GetUserTradesByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetUserTradesByInstrumentRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_user_trades_by_instrument
            (
                instrument_name: reqdto.instrument_name,
                start_seq: reqdto.start_seq,
                end_seq: reqdto.end_seq,
                count: reqdto.count,
                include_old: reqdto.include_old,
                sorting: reqdto.sorting,
                ct
            );
            // map response
            GetUserTradesByInstrumentResponse response = this.mapper.Map<GetUserTradesByInstrumentResponse>(responsedto);
            // validate response
            new GetUserTradesByInstrumentResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<IList<Trade>> GetUserTradesByOrder(GetUserTradesByOrderRequest request, CancellationToken ct)
        {
            // validate request
            new GetUserTradesByOrderRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetUserTradesByOrderRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_user_trades_by_order
            (
                order_id: reqdto.order_id,
                sorting: reqdto.sorting,
                ct
            );
            // map response
            IList<Trade> response = this.mapper.Map<IList<Trade>>(responsedto);
            // validate response
            foreach (var trade in response)
            {
                new Trade.Validator().ValidateAndThrow(trade);
            }
            // return
            return response;
        }


        //------------------------------------------------------------------------------------------------
    }
}
