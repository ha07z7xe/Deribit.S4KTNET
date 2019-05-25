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

        Task<GetContractSizeResponse> GetContractSize(GetContractSizeRequest request, CancellationToken ct = default);

        Task<IList<Currency>> GetCurrencies(CancellationToken ct = default);

        Task<GetIndexResponse> GetIndex(GetIndexRequest request, CancellationToken ct = default);

        Task<IList<Instrument>> GetInstruments(GetInstrumentsRequest request, CancellationToken ct = default);
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

        public async Task<GetContractSizeResponse> GetContractSize(GetContractSizeRequest request, CancellationToken ct)
        {
            // validate request
            new GetContractSizeRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetContractSizeRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.get_contract_size(reqdto.instrument_name, ct);
            // map response
            GetContractSizeResponse response = mapper.Map<GetContractSizeResponse>(responsedto);
            // validate response
            new GetContractSizeResponse.Validator().Validate(response);
            // return
            return response;
        }

        public async Task<IList<Currency>> GetCurrencies(CancellationToken ct)
        {
            // execute request
            var responsedto = await this.rpcproxy.get_currencies(ct);
            // map response
            IList<Currency> response = mapper.Map<IList<Currency>>(responsedto);
            // return
            return response;
        }

        public async Task<GetIndexResponse> GetIndex(GetIndexRequest request, CancellationToken ct)
        {
            // validate request
            new GetIndexRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetIndexRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.get_index(reqdto.currency, ct);
            // map response
            GetIndexResponse response = mapper.Map<GetIndexResponse>(responsedto);
            // validate response
            new GetIndexResponse.Validator().Validate(response);
            // return
            return response;
        }

        public async Task<IList<Instrument>> GetInstruments(GetInstrumentsRequest request, CancellationToken ct)
        {
            // validate request
            new GetInstrumentsRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetInstrumentsRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.get_instruments
            (
                currency: reqdto.currency,
                kind: reqdto.kind,
                expired: reqdto.expired,
                ct
            );
            // map response
            IList<Instrument> response = mapper.Map<IList<Instrument>>(responsedto);
            // return
            return response;
        }
        //------------------------------------------------------------------------------------------------
    }
}
