using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Nethereum.Parity.RPC.Trace
{
    /// <Summary>
    ///     Returns traces created at given block
    /// </Summary>
    public class TraceBlock : RpcRequestResponseHandler<JArray>
    {
        public TraceBlock(IClient client) : base(client, ApiMethods.trace_block.ToString())
        {
        }

        public async Task<JArray> SendRequestAsync(HexBigInteger blockNumber, object id = null,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SendRequestAsync(id, cancellationToken, blockNumber);
        }

        public RpcRequest BuildRequest(HexBigInteger blockNumber, object id = null)
        {
            return base.BuildRequest(id, blockNumber);
        }
    }
}