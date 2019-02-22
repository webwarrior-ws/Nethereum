using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Newtonsoft.Json.Linq;

namespace Nethereum.Geth.RPC.Debug
{
    /// <Summary>
    ///     Retrieves the state that corresponds to the block number and returns a list of accounts (including storage and
    ///     code).
    /// </Summary>
    public class DebugDumpBlock : RpcRequestResponseHandler<JObject>
    {
        public DebugDumpBlock(IClient client) : base(client, ApiMethods.debug_dumpBlock.ToString())
        {
        }

        public RpcRequest BuildRequest(ulong blockNumber, object id = null)
        {
            return base.BuildRequest(id, blockNumber);
        }

        public Task<JObject> SendRequestAsync(ulong blockNumber, object id = null,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken, blockNumber);
        }
    }
}