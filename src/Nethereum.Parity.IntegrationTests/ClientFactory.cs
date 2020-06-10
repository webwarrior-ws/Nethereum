using System;
using JsonRpcSharp.Client;

namespace Nethereum.Parity.IntegrationTests
{
    public class ClientFactory
    {
        public static IClient GetClient(TestSettings settings)
        {
            var url = settings.GetRPCUrl();
            return new HttpClient(new Uri(url));
        }
    }
}