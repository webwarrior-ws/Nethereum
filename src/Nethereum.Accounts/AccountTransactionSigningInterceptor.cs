using System;
using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Web3.Accounts
{
    public class AccountTransactionSigningInterceptor : RequestInterceptor
    {
        private readonly AccountSignerTransactionManager signer;

        public AccountTransactionSigningInterceptor(string privateKey, IClient client)
        {
            signer = new AccountSignerTransactionManager(client, privateKey);
        }

        public override async Task<object> InterceptSendRequestAsync<TResponse>(
            Func<RpcRequest, string, CancellationToken, Task<TResponse>> interceptedSendRequestAsync, RpcRequest request,
            string route = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request.Method == "eth_sendTransaction")
            {
                var transaction = (TransactionInput) request.RawParameters[0];
                return await SignAndSendTransactionAsync(transaction, cancellationToken).ConfigureAwait(false);
            }
            return await base.InterceptSendRequestAsync(interceptedSendRequestAsync, request, route, cancellationToken)
                .ConfigureAwait(false);
        }

        public override async Task<object> InterceptSendRequestAsync<T>(
            Func<string, string, CancellationToken, object[], Task<T>> interceptedSendRequestAsync, string method,
            string route = null, CancellationToken cancellationToken = default(CancellationToken),
            params object[] paramList)
        {
            if (method == "eth_sendTransaction")
            {
                var transaction = (TransactionInput) paramList[0];
                return await SignAndSendTransactionAsync(transaction, cancellationToken).ConfigureAwait(false);
            }
            return await base.InterceptSendRequestAsync(interceptedSendRequestAsync, method, route, cancellationToken,
                paramList)
                .ConfigureAwait(false);
        }

        private async Task<string> SignAndSendTransactionAsync(TransactionInput transaction,
                                                               CancellationToken cancellationToken = default(CancellationToken))
        {
            return await signer.SendTransactionAsync(transaction, cancellationToken).ConfigureAwait(false);
        }
    }
}