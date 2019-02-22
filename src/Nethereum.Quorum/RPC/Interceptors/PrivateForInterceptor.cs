using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Nethereum.Quorum.RPC.DTOs;
using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json.Linq;

namespace Nethereum.Quorum.RPC.Interceptors
{
    public class PrivateForInterceptor : RequestInterceptor
    {
        private readonly List<string> privateFor;
        private readonly string privateFrom;

        public PrivateForInterceptor(List<string> privateFor, string privateFrom)
        {
            this.privateFor = privateFor;
            this.privateFrom = privateFrom;
        }

        public override async Task<object> InterceptSendRequestAsync<T>(
            Func<RpcRequest, string, CancellationToken, Task<T>> interceptedSendRequestAsync, RpcRequest request,
            string route = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request.Method == "eth_sendTransaction")
            {
                var transaction = (TransactionInput) request.RawParameters[0];
                var privateTransaction = new PrivateTransactionInput(transaction, privateFor.ToArray(), privateFrom);
                return await interceptedSendRequestAsync(new RpcRequest(request.Id, request.Method, privateTransaction),
                                                         route,
                                                         cancellationToken)
                                                            .ConfigureAwait(false);
            }
            return await interceptedSendRequestAsync(request, route, cancellationToken)
                .ConfigureAwait(false);
        }

        public override async Task<object> InterceptSendRequestAsync<T>(
            Func<string, string, CancellationToken, object[], Task<T>> interceptedSendRequestAsync, string method,
            string route = null,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] paramList)
        {
            if (method == "eth_sendTransaction")
            {
                var transaction = (TransactionInput) paramList[0];
                var privateTransaction = new PrivateTransactionInput(transaction, privateFor.ToArray(), privateFrom);
                paramList[0] = privateTransaction;
                return await interceptedSendRequestAsync(method, route, cancellationToken, paramList)
                    .ConfigureAwait(false);
            }

            return await interceptedSendRequestAsync(method, route, cancellationToken, paramList)
                .ConfigureAwait(false);
        }

    }
}