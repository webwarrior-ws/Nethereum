using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.Geth.RPC.Debug
{
    /// <Summary>
    ///     Starts writing a Go runtime trace to the given file.
    /// </Summary>
    public class DebugStartGoTrace : RpcRequestResponseHandler<object>
    {
        public DebugStartGoTrace(IClient client) : base(client, ApiMethods.debug_startGoTrace.ToString())
        {
        }

        public RpcRequest BuildRequest(string filePath, object id = null)
        {
            return base.BuildRequest(id, filePath);
        }

        public Task<object> SendRequestAsync(string filePath, object id = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken, filePath);
        }
    }
}