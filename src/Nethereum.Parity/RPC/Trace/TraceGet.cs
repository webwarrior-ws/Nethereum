using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Nethereum.Parity.RPC.Trace
{
    /// <Summary>
    ///     Returns trace at given position.
    /// </Summary>
    public class TraceGet : RpcRequestResponseHandler<JObject>
    {
        public TraceGet(IClient client) : base(client, ApiMethods.trace_get.ToString())
        {
        }

        public async Task<JObject> SendRequestAsync(string transactionHash, HexBigInteger[] index, object id = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SendRequestAsync(id, cancellationToken, transactionHash, index);
        }

        public RpcRequest BuildRequest(string transactionHash, HexBigInteger[] index, object id = null)
        {
            return base.BuildRequest(id, transactionHash, index);
        }
    }
}