using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.MarketData
{
    public interface IDeribitMarketDataService
    {
        Task<IList<BookSummary>> GetBookSummaryByInstrument(GetBookSummaryByInstrumentRequest request, CancellationToken ct = default);
    }

    internal class DeribitMarketDataService : IDeribitMarketDataService
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

        public DeribitMarketDataService(DeribitService deribit, IMapper mapper, 
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
                builder.RegisterType<DeribitMarketDataService>()
                    .AsSelf()
                    .As<IDeribitMarketDataService>()
                    .SingleInstance();
            }
        }

        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------


        public async Task<IList<BookSummary>> GetBookSummaryByInstrument(GetBookSummaryByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new GetBookSummaryByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetBookSummaryByInstrumentRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.get_book_summary_by_instrument(reqdto.instrument_name, ct);
            // map response
            IList<BookSummary> response = mapper.Map<IList<BookSummary>>(responsedto);
            // validate response
            var bookvalidator = new BookSummary.Validator();
            foreach (var bs in response)
            {
                bookvalidator.ValidateAndThrow(bs);
            }
            // return
            return response;
        }
        //------------------------------------------------------------------------------------------------
    }
}
