using System;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.RPC.Tests.Testers
{
    public interface IRPCRequestTester
    {
        Task<object> ExecuteTestAsync(IClient client);
        Type GetRequestType();
    }
}