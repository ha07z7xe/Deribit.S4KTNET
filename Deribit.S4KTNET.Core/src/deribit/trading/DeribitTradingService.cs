using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Trading
{
    public interface IDeribitTradingService
    {
        Task<BuySellResponse> buy(BuySellRequest request, CancellationToken ct = default);

        Task<BuySellResponse> sell(BuySellRequest request, CancellationToken ct = default);
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

        public async Task<BuySellResponse> buy(BuySellRequest request, CancellationToken ct = default)
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

        public async Task<BuySellResponse> sell(BuySellRequest request, CancellationToken ct = default)
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

        //------------------------------------------------------------------------------------------------
    }
}
