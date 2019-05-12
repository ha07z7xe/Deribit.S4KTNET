using Deribit.S4KTNET.Core;
using System;

namespace Deribit.S4KTNET.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // create config
            DeribitConfig deribitconfig = new DeribitConfig
            {
                Environment = DeribitEnvironment.Test,
            };

            // construct services
            DeribitService deribit = new DeribitService(deribitconfig);

            // connect
            deribit.Connect(default).Wait();

            // dispose
            deribit.Dispose();
        }
    }
}
