using System;
using System.Threading;
using System.Threading.Tasks;
 
using JsonRpcSharp.Client;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.RPC.Eth.Transactions
{
    public class EthSendTransaction : RpcRequestResponseHandler<string>
    {
        public EthSendTransaction(IClient client) : base(client, ApiMethods.eth_sendTransaction.ToString())
        {
        }

        public Task<string> SendRequestAsync(TransactionInput input,
                                             object id = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return base.SendRequestAsync(id, cancellationToken, input);
        }

        public RpcRequest BuildRequest(TransactionInput input, object id = null)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return base.BuildRequest(id, input);
        }
    }
}