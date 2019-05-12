using Autofac;
using AutoMapper;
using Deribit.S4KTNET.Core.JsonRpc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deribit.S4KTNET.Core.Supporting
{
    public interface IDeribitSupportingService
    {
        Task<long> get_time(CancellationToken ct = default);

        Task<HelloResponse> hello(HelloRequest request, CancellationToken ct = default);

        Task<TestResponse> test(TestRequest request, CancellationToken ct = default);
    }

    internal class DeribitSupportingService : IDeribitSupportingService
    {
        //------------------------------------------------------------------------------------------------
        // dependencies
        //------------------------------------------------------------------------------------------------

        private readonly DeribitService deribit;
        private readonly IMapper mapper;
        private readonly IDeribitJsonRpcProxy rpcproxy;

        //------------------------------------------------------------------------------------------------
        // construction
        //------------------------------------------------------------------------------------------------

        public DeribitSupportingService(DeribitService deribit, IMapper mapper, IDeribitJsonRpcProxy rpcproxy)
        {
            this.deribit = deribit;
            this.mapper = mapper;
            this.rpcproxy = rpcproxy;
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


        public Task<long> get_time(CancellationToken ct)
        {
            return this.deribit.JsonRpc2.RpcProxy.get_time(ct);
        }

        public async Task<HelloResponse> hello(HelloRequest request, CancellationToken ct)
        {
            // map request
            HelloRequestDto requestdto = mapper.Map<HelloRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.hello(requestdto, ct);
            // map response
            HelloResponse response = mapper.Map<HelloResponse>(responsedto);
            // return
            return response;
        }

        public async Task<TestResponse> test(TestRequest request, CancellationToken ct)
        {
            // map request
            TestRequestDto requestdto = mapper.Map<TestRequestDto>(request);
            // execute request
            var responsedto = await this.rpcproxy.test(requestdto, ct);
            // map response
            TestResponse response = mapper.Map<TestResponse>(responsedto);
            // return
            return response;
        }
        //------------------------------------------------------------------------------------------------

    }
}
