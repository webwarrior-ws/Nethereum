using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Newtonsoft.Json.Linq;

namespace Nethereum.Geth.RPC.Debug
{
    /// <Summary>
    ///     Similar to debug_traceBlock, traceBlockByHash accepts a block hash and will replay the block that is already
    ///     present in the database.
    /// </Summary>
    public class DebugTraceBlockByHash : RpcRequestResponseHandler<JObject>
    {
        public DebugTraceBlockByHash(IClient client) : base(client, ApiMethods.debug_traceBlockByHash.ToString())
        {
        }

        public RpcRequest BuildRequest(string hash, object id = null)
        {
            return base.BuildRequest(id, hash);
        }

        public Task<JObject> SendRequestAsync(string hash, object id = null,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken, hash);
        }
    }
}