using System;
using System.Threading;
using System.Threading.Tasks;
using JsonRpcSharp.Client;

namespace Nethereum.RPC.UnitTests.InterceptorTests
{
    public class OverridingInterceptorMock:RequestInterceptor
    {
        public override async Task<object> InterceptSendRequestAsync<T>(Func<RpcRequest, string, CancellationToken, Task<T>> interceptedSendRequestAsync,
                                                                        RpcRequest request,
                                                                        string route = null,
                                                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request.Method == "eth_accounts")
            {
                return new string[] { "hello", "hello2"};
            }

            if (request.Method == "eth_getCode")
            {
                return "the code";
            }
            return await interceptedSendRequestAsync(request, route, cancellationToken);
        }


        public override async Task<object> InterceptSendRequestAsync<T>(Func<string, string, CancellationToken, object[], Task<T>> interceptedSendRequestAsync,
            string method,
            string route = null,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] paramList)
        {
            if (method == "eth_accounts")
            {
                return new string[] { "hello", "hello2"};
            }

            if (method == "eth_getCode")
            {
                return "the code";
            }

            return await interceptedSendRequestAsync(method, route, cancellationToken, paramList);
        }
    }
}