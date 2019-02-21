using JsonRpcSharp.Client;

namespace Nethereum.RPC
{
    public class RpcClientWrapper
    {
        public RpcClientWrapper(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; protected set; }
    }
}