using System;
using JsonRpcSharp.Client;
using Nethereum.RPC.Tests.Testers;

namespace Nethereum.RPC.Tests
{
    public class ClientFactory
    {
        public static IClient GetClient(TestSettings settings)
        {
            var url = settings.GetRPCUrl();
            return new RpcClient(new Uri(url));
        }
    }
}