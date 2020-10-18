using System;
using JsonRpcSharp.Client;

namespace Nethereum.Parity.IntegrationTests
{
    public class ClientFactory
    {
        private static TimeSpan defaultTimeOutForTests = TimeSpan.FromSeconds(30.0);

        public static IClient GetClient(TestSettings settings)
        {
            var url = settings.GetRPCUrl();
            return new HttpClient(new Uri(url), defaultTimeOutForTests);
        }
    }
}