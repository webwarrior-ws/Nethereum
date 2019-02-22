using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using System.Threading;

namespace Nethereum.Quorum.RPC
{
    public class QuorumCanonicalHash : RpcRequestResponseHandler<string>
    {
        public QuorumCanonicalHash(IClient client) : base(client, ApiMethods.quorum_canonicalHash.ToString())
        {
        }

        public Task<string> SendRequestAsync(long blockNumber, object id = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken, blockNumber);
        }

        public RpcRequest BuildRequest(long blockNumber, object id = null)
        {
            return base.BuildRequest(id, blockNumber);
        }
    }
}