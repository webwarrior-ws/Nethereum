using System;
using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.Geth.RPC.Miner
{
    /// <Summary>
    ///     Start the CPU mining process with the given number of threads and generate a new DAG if need be.
    /// </Summary>
    public class MinerStart : RpcRequestResponseHandler<bool>
    {
        public MinerStart(IClient client) : base(client, ApiMethods.miner_start.ToString())
        {
        }

        public RpcRequest BuildRequest(int number, object id = null)
        {
            if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));
            return base.BuildRequest(id, number);
        }

        public Task<bool> SendRequestAsync(int number, object id = null,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));
            return base.SendRequestAsync(id, cancellationToken, number);
        }

        public Task<bool> SendRequestAsync(object id = null,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SendRequestAsync(id, cancellationToken, 1);
        }
    }
}