using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    public interface IDeribitAccountManagementService
    {
        Task<Account> GetAccountSummary(GetAccountSummaryRequest request, CancellationToken ct = default);

        Task<Position> GetPosition(GetPositionRequest request, CancellationToken ct = default);

        Task<IList<Position>> GetPositions(GetPositionsRequest request, CancellationToken ct = default);
    }

    internal class DeribitAccountManagementService : IDeribitAccountManagementService
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

        public DeribitAccountManagementService(DeribitService deribit, IMapper mapper,
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
                builder.RegisterType<DeribitAccountManagementService>()
                    .AsSelf()
                    .As<IDeribitAccountManagementService>()
                    .SingleInstance();
            }
        }

        //------------------------------------------------------------------------------------------------
        // disposal
        //------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            
        }

        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------

        public async Task<Account> GetAccountSummary(GetAccountSummaryRequest request, CancellationToken ct)
        {
            // validate
            new GetAccountSummaryRequest.Validator().ValidateAndThrow(request);
            // map request
            GetAccountSummaryRequestDto requestDto = mapper.Map<GetAccountSummaryRequestDto>(request);
            // execute request
            var accountdto = await this.rpcproxy.get_account_summary(requestDto.currency, requestDto.extended, ct);
            // map response
            Account account = mapper.Map<Account>(accountdto);
            // validate response
            new Account.Validator().ValidateAndThrow(account);
            // return
            return account;
        }

        public async Task<Position> GetPosition(GetPositionRequest request, CancellationToken ct)
        {
            // validate
            new GetPositionRequest.Validator().ValidateAndThrow(request);
            // map request
            GetPositionRequestDto requestDto = mapper.Map<GetPositionRequestDto>(request);
            // execute request
            var positiondto = await this.rpcproxy.get_position(requestDto.instrument_name, ct);
            // map response
            Position position = mapper.Map<Position>(positiondto);
            // return
            return position;
        }

        public async Task<IList<Position>> GetPositions(GetPositionsRequest request, CancellationToken ct)
        {
            // validate
            new GetPositionsRequest.Validator().ValidateAndThrow(request);
            // map request
            GetPositionsRequestDto requestDto = mapper.Map<GetPositionsRequestDto>(request);
            // execute request
            var positiondtos = await this.rpcproxy.get_positions
            (
                requestDto.currency, 
                requestDto.kind, 
                ct
            );
            // map response
            IList<Position> positions = mapper.Map<IList<Position>>(positiondtos);
            // return
            return positions;
        }

        //------------------------------------------------------------------------------------------------

    }
}
