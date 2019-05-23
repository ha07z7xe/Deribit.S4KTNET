using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
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

        Task<GenericResponse> CancelAll(CancellationToken ct = default);

        Task<GenericResponse> CancelAllByCurrency(CancelAllByCurrencyRequest request, CancellationToken ct = default);

        Task<GenericResponse> CancelAllByInstrument(CancelAllByInstrumentRequest request, CancellationToken ct = default);

        Task<ClosePositionResponse> ClosePosition(ClosePositionRequest request, CancellationToken ct = default);

        Task<GetMarginsResponse> GetMargins(GetMarginsRequest request, CancellationToken ct = default);

        Task<IList<Order>> GetOpenOrdersByInstrument(GetOpenOrdersByInstrumentRequest request, CancellationToken ct = default);

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
        private readonly IDeribitJsonRpcProxy rpcproxy;
        private readonly StreamJsonRpc.JsonRpc jsonrpc;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitTradingService(DeribitService deribit, IMapper mapper, 
            IDeribitJsonRpcProxy rpcproxy, StreamJsonRpc.JsonRpc jsonrpc)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.rpcproxy = rpcproxy;
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
            var responsedto = await this.rpcproxy.buy
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
            var responsedto = await this.rpcproxy.sell
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
            var responsedto = await this.rpcproxy.edit
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
            var orderdto = await this.rpcproxy.cancel(reqdto.order_id, ct);
            // map response
            Order order = mapper.Map<Order>(orderdto);
            // return
            return order;
        }

        public async Task<GenericResponse> CancelAll(CancellationToken ct)
        {
            // execute request
            var responsedto = await this.rpcproxy.cancel_all(ct);
            // map response
            GenericResponse response = this.mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> CancelAllByCurrency(CancelAllByCurrencyRequest request, CancellationToken ct)
        {
            // validate request
            new CancelAllByCurrencyRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<CancelAllByCurrencyRequestDto>(request);
            // validate request dto
            new CancelAllByCurrencyRequestDto.Validator().ValidateAndThrow(reqdto);
            // execute request
            var responsedto = await this.rpcproxy.cancel_all_by_currency
            (
                currency: reqdto.currency,
                kind: reqdto.kind,
                type: reqdto.type,
                ct
            );
            // map response
            GenericResponse response = this.mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
        }

        public async Task<GenericResponse> CancelAllByInstrument(CancelAllByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new CancelAllByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<CancelAllByInstrumentRequestDto>(request);
            // validate request dto
            new CancelAllByInstrumentRequestDto.Validator().ValidateAndThrow(reqdto);
            // execute request
            var responsedto = await this.rpcproxy.cancel_all_by_instrument
            (
                instrument_name: reqdto.instrument_name,
                type: reqdto.type,
                ct
            );
            // map response
            GenericResponse response = this.mapper.Map<GenericResponse>(responsedto);
            // return
            return response;
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
            var responsedto = await this.rpcproxy.close_position
            (
                instrument_name: reqdto.instrument_name,
                type: reqdto.type,
                price: reqdto.price,
                ct
            );
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
            var responsedto = await this.rpcproxy.get_margins
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

        public async Task<IList<Order>> GetOpenOrdersByInstrument(GetOpenOrdersByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new GetOpenOrdersByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOpenOrdersByInstrumentRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.get_open_orders_by_instrument
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

        public async Task<Order> GetOrderState(GetOrderStateRequest request, CancellationToken ct)
        {
            // validate request
            new GetOrderStateRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOrderStateRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.get_order_state
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
            var responsedto = await this.rpcproxy.get_user_trades_by_instrument
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
            var responsedto = await this.rpcproxy.get_user_trades_by_order
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
