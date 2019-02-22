using System;
using System.Threading.Tasks;
using JsonRpcSharp.Client;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading;

namespace Nethereum.RPC.TransactionManagers
{
    public class TransactionManager : TransactionManagerBase
    {
        public override BigInteger DefaultGas { get; set; }

        public TransactionManager(IClient client)
        {
            this.Client = client;
        }

#if !DOTNET35
        
        public override Task<string> SignTransactionAsync(TransactionInput transaction,
                                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new InvalidOperationException("Default transaction manager cannot sign offline transactions");
        }

        public override Task<string> SendTransactionAsync(TransactionInput transactionInput,
                                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Client == null) throw new NullReferenceException("Client not configured");
            if (transactionInput == null) throw new ArgumentNullException(nameof(transactionInput));
            SetDefaultGasPriceAndCostIfNotSet(transactionInput);
            return new EthSendTransaction(Client).SendRequestAsync(transactionInput, cancellationToken);
        }
#endif
    }

}