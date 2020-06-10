using System;
using JsonRpcSharp.Client;

namespace Nethereum.ENS.IntegrationTests
{
    public class ClientFactory
    {
        public static IClient GetClient()
        {
//#if NET462
           // var client = new IpcClient("geth.ipc");
            //return client;
//#else      
          return new HttpClient(new Uri("http://localhost:8545/"));
//#endif
           
        }
    }
}
