using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.Geth.RPC.Debug
{
    /// <Summary>
    ///     Turns on CPU profiling indefinitely, writing to the given file.
    /// </Summary>
    public class DebugStartCPUProfile : RpcRequestResponseHandler<object>
    {
        public DebugStartCPUProfile(IClient client) : base(client, ApiMethods.debug_startCPUProfile.ToString())
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