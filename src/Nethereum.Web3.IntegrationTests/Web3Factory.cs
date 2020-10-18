using System;

namespace Nethereum.Web3.IntegrationTests
{
    public static class Web3Factory
    {
        private static TimeSpan defaultTimeOutForTests = TimeSpan.FromSeconds(30.0);

        public static Web3 GetWeb3()
        {
            return new Web3(AccountFactory.GetAccount(), defaultTimeOutForTests);
        }

        public static Web3 GetWeb3Managed()
        {
            return new Web3(AccountFactory.GetManagedAccount(), defaultTimeOutForTests);
        }
    }
}