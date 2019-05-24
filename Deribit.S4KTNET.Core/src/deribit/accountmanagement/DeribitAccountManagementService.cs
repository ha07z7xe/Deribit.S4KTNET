using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using Newtonsoft.Json.Linq;
using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    public interface IDeribitAccountManagementService
    {
        Task<Position> GetPosition(GetPositionRequest request, CancellationToken ct = default);
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

        //------------------------------------------------------------------------------------------------

    }
}
