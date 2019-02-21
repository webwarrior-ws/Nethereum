using System;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.Parity.IntegrationTests
{
    public interface IRPCRequestTester
    {
        Task<object> ExecuteTestAsync(IClient client);
        Type GetRequestType();
    }
}