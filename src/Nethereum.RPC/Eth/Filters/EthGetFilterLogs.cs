using System;
using System.Threading.Tasks;
 
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using System.Threading;

namespace Nethereum.RPC.Eth.Filters
{
    /// <Summary>
    ///     eth_getFilterLogs
    ///     Returns an array of all logs matching filter with given id.
    ///     Parameters
    ///     QUANTITY - The filter id.
    ///     params: [
    ///     "0x16" // 22
    ///     ]
    ///     Returns
    ///     See eth_getFilterChanges
    ///     Example
    ///     Request
    ///     curl -X POST --data '{"jsonrpc":"2.0","method":"eth_getFilterLogs","params":["0x16"],"id":74}'
    /// </Summary>
    public class EthGetFilterLogsForBlockOrTransaction : RpcRequestResponseHandler<string[]>
    {
        public EthGetFilterLogsForBlockOrTransaction(IClient client)
            : base(client, ApiMethods.eth_getFilterLogs.ToString())
        {
        }

        public Task<string[]> SendRequestAsync(HexBigInteger filterId,
                                               object id = null,
                                               CancellationToken cancellationToken = default(CancellationToken))
        {
            if (filterId == null) throw new ArgumentNullException(nameof(filterId));
            return base.SendRequestAsync(id, cancellationToken, filterId);
        }

        public RpcRequest BuildRequest(HexBigInteger filterId, object id = null)
        {
            if (filterId == null) throw new ArgumentNullException(nameof(filterId));
            return base.BuildRequest(id, filterId);
        }
    }
}