using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.Geth.RPC.Debug
{
    /// <Summary>
    ///     Retrieves and returns the RLP encoded block by number.
    /// </Summary>
    public class DebugGetBlockRlp : RpcRequestResponseHandler<string>
    {
        public DebugGetBlockRlp(IClient client) : base(client, ApiMethods.debug_getBlockRlp.ToString())
        {
        }

        public RpcRequest BuildRequest(ulong blockNumber, object id = null)
        {
            return base.BuildRequest(id, blockNumber);
        }

        public Task<string> SendRequestAsync(ulong blockNumber, object id = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken, blockNumber);
        }
    }
}