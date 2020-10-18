using System;
using JsonRpcSharp.Client;

namespace Nethereum.ENS.IntegrationTests
{
    public class ClientFactory
    {
        private static TimeSpan defaultTimeOutForTests = TimeSpan.FromSeconds(30.0);

        public static IClient GetClient()
        {
//#if NET462
           // var client = new IpcClient("geth.ipc");
            //return client;
//#else      
          return new HttpClient(new Uri("http://localhost:8545/"), defaultTimeOutForTests);
//#endif
           
        }
    }
}
