using System;
using System.Threading.Tasks;
 
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using Nethereum.RPC.Eth.DTOs;
using System.Threading;

namespace Nethereum.RPC.Eth.Blocks
{
    /// <Summary>
    ///     eth_getBlockTransactionCountByNumber
    ///     Returns the number of transactions in a block from a block matching the given block number.
    ///     Parameters
    ///     QUANTITY|TAG - integer of a block number, or the string "earliest", "latest" or "pending", as in the default block
    ///     parameter.
    ///     params: [
    ///     '0xe8', // 232
    ///     ]
    ///     Returns
    ///     QUANTITY - integer of the number of transactions in this block.
    ///     Example
    ///     Request
    ///     curl -X POST --data '{"jsonrpc":"2.0","method":"eth_getBlockTransactionCountByNumber","params":["0xe8"],"id":1}'
    ///     Result
    ///     {
    ///     "id":1,
    ///     "jsonrpc": "2.0",
    ///     "result": "0xa" // 10
    ///     }
    /// </Summary>
    public class EthGetBlockTransactionCountByNumber : RpcRequestResponseHandler<HexBigInteger>
    {
        public EthGetBlockTransactionCountByNumber(IClient client)
            : base(client, ApiMethods.eth_getBlockTransactionCountByNumber.ToString())
        {
        }

        public Task<HexBigInteger> SendRequestAsync(BlockParameter block,
                                                    object id = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            return base.SendRequestAsync(id, cancellationToken, block);
        }

        public Task<HexBigInteger> SendRequestAsync(object id = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendRequestAsync(BlockParameter.CreateLatest(), id, cancellationToken);
        }

        public RpcRequest BuildRequest(BlockParameter block, object id = null)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            return base.BuildRequest(id, block);
        }
    }
}