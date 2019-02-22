using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.RPC.Infrastructure
{
    public class GenericRpcRequestResponseHandlerNoParam<TResponse> : RpcRequestResponseHandlerNoParam<TResponse>
    {
        public GenericRpcRequestResponseHandlerNoParam(IClient client, string methodName) : base(client, methodName)
        {
        }

        public new Task<TResponse> SendRequestAsync(object id = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken);
        }
    }
}