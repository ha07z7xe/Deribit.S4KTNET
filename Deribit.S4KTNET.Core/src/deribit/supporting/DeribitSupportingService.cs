using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Supporting
{
    public interface IDeribitSupportingService
    {
        Task<GetTimeResponse> GetTime(CancellationToken ct = default);

        Task<HelloResponse> Hello(HelloRequest request, CancellationToken ct = default);

        Task<TestResponse> Test(TestRequest request, CancellationToken ct = default);
    }

    internal class DeribitSupportingService : IDeribitSupportingService
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

        public DeribitSupportingService(DeribitService deribit, IMapper mapper, 
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
                builder.RegisterType<DeribitSupportingService>()
                    .AsSelf()
                    .As<IDeribitSupportingService>()
                    .SingleInstance();
            }
        }


        //------------------------------------------------------------------------------------------------
        // api
        //------------------------------------------------------------------------------------------------


        public async Task<GetTimeResponse> GetTime(CancellationToken ct)
        {
            // execute request
            var responsedto = await this.rpcproxy.get_time(ct);
            // map response
            GetTimeResponse response = mapper.Map<GetTimeResponse>(responsedto);
            // return
            return response;
        }

        public async Task<HelloResponse> Hello(HelloRequest request, CancellationToken ct)
        {
            // validate
            new HelloRequest.Validator().ValidateAndThrow(request);
            // map request
            HelloRequestDto requestdto = mapper.Map<HelloRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.hello
            (
                client_name: requestdto.client_name,
                client_version: requestdto.client_version,
                ct
            );
            // map response
            HelloResponse response = mapper.Map<HelloResponse>(responsedto);
            // return
            return response;
        }

        public async Task<TestResponse> Test(TestRequest request, CancellationToken ct)
        {
            // validate
            new TestRequest.Validator().ValidateAndThrow(request);
            // map request
            TestRequestDto requestdto = mapper.Map<TestRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.test
            (
                expected_result: requestdto.expected_result,
                ct
            );
            // map response
            TestResponse response = mapper.Map<TestResponse>(responsedto);
            // return
            return response;
        }
        //------------------------------------------------------------------------------------------------

    }
}
