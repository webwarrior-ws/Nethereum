using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Newtonsoft.Json.Linq;

namespace Nethereum.Geth.RPC.Debug
{
    /// <Summary>
    ///     Similar to debug_traceBlock, traceBlockByNumber accepts a block number and will replay the block that is already
    ///     present in the database.
    /// </Summary>
    public class DebugTraceBlockByNumber : RpcRequestResponseHandler<JObject>
    {
        public DebugTraceBlockByNumber(IClient client) : base(client, ApiMethods.debug_traceBlockByNumber.ToString())
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