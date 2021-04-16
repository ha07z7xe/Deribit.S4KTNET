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

        Task<GetLastTradesByInstrumentResponse> GetLastTradesByInstrument(GetLastTradesByInstrumentRequest request, CancellationToken ct = default);

        Task<OrderBook> GetOrderBook(GetOrderBookRequest request, CancellationToken ct = default);

        Task<Ticker> Ticker(TickerRequest request, CancellationToken ct = default);
        
        Task<TradingViewChartData> GetTradingviewChartData(GetTradingViewChartData request, CancellationToken ct = default);
    }

    internal class DeribitMarketDataService : IDeribitMarketDataService
    {
        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcService jsonrpc;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitMarketDataService(DeribitService deribit, IMapper mapper,
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
            var responsedto = await this.jsonrpc.RpcProxy.get_book_summary_by_instrument(reqdto.instrument_name, ct);
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
            var responsedto = await this.jsonrpc.RpcProxy.get_contract_size(reqdto.instrument_name, ct);
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
            var responsedto = await this.jsonrpc.RpcProxy.get_currencies(ct);
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
            var responsedto = await this.jsonrpc.RpcProxy.get_index(reqdto.currency, ct);
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
            var responsedto = await this.jsonrpc.RpcProxy.get_instruments
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

        public async Task<GetLastTradesByInstrumentResponse> GetLastTradesByInstrument(GetLastTradesByInstrumentRequest request, CancellationToken ct)
        {
            // validate request
            new GetLastTradesByInstrumentRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetLastTradesByInstrumentRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_last_trades_by_instrument
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
            GetLastTradesByInstrumentResponse response = mapper.Map<GetLastTradesByInstrumentResponse>(responsedto);
            // validate response
            new GetLastTradesByInstrumentResponse.Validator().ValidateAndThrow(response);
            // return
            return response;
        }

        public async Task<OrderBook> GetOrderBook(GetOrderBookRequest request, CancellationToken ct)
        {
            // validate request
            new GetOrderBookRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetOrderBookRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_order_book(reqdto.instrument_name, reqdto.depth, ct);
            // map response
            OrderBook book = mapper.Map<OrderBook>(responsedto);
            // validate response
            new OrderBook.Validator().ValidateAndThrow(book);
            // return
            return book;
        }

        public async Task<Ticker> Ticker(TickerRequest request, CancellationToken ct)
        {
            // validate request
            new TickerRequest.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<TickerRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.ticker(reqdto.instrument_name, ct);
            // map response
            Ticker ticker = mapper.Map<Ticker>(responsedto);
            // validate response
            new Ticker.Validator().ValidateAndThrow(ticker);
            // return
            return ticker;
        }

        public async Task<TradingViewChartData> GetTradingviewChartData(GetTradingViewChartData request, CancellationToken ct)
        {
            // validate request
            new GetTradingViewChartData.Validator().ValidateAndThrow(request);
            // map request
            var reqdto = this.mapper.Map<GetTradingviewChartDataRequestDto>(request);
            // execute request
            var responsedto = await this.jsonrpc.RpcProxy.get_tradingview_chart_data(reqdto.instrument_name,
                reqdto.start_timestamp,
                reqdto.end_timestamp,
                reqdto.resolution,
                ct);

            Serilog.Log.Information("reqdto.resolution.ToString()");
            // map response
            TradingViewChartData chartdata = mapper.Map<TradingViewChartData>(responsedto);
            // validate response
            new TradingViewChartData.Validator().ValidateAndThrow(chartdata);
            // return
            return chartdata;
        }

        //------------------------------------------------------------------------------------------------
    }
}
