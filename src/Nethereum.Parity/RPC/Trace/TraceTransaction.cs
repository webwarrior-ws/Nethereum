using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Newtonsoft.Json.Linq;

namespace Nethereum.Parity.RPC.Trace
{
    /// <Summary>
    ///     Returns all traces of given transaction
    /// </Summary>
    public class TraceTransaction : RpcRequestResponseHandler<JObject>
    {
        public TraceTransaction(IClient client) : base(client, ApiMethods.trace_transaction.ToString())
        {
        }

        public async Task<JObject> SendRequestAsync(string transactionHash, object id = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SendRequestAsync(id, cancellationToken, transactionHash);
        }

        public RpcRequest BuildRequest(string transactionHash, object id = null)
        {
            return base.BuildRequest(id, transactionHash);
        }
    }
}