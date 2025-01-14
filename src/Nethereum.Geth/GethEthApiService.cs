﻿using Nethereum.Geth.RPC.GethEth;
using JsonRpcSharp.Client;
using Nethereum.RPC;

namespace Nethereum.Geth
{
    public class GethEthApiService : RpcClientWrapper
    {
        public GethEthApiService(IClient client) : base(client)
        {
            PendingTransactions = new EthPendingTransactions(client);
        }

        public EthPendingTransactions PendingTransactions { get; }
    }
}