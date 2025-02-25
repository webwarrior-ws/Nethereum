﻿using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using System.Numerics;
using System.Threading;
using Nethereum.RPC.Accounts;
using Nethereum.RPC.Eth;
using Nethereum.RPC.TransactionReceipts;

namespace Nethereum.RPC.TransactionManagers
{
    public abstract class TransactionManagerBase : ITransactionManager
    {
        public virtual IClient Client { get; set; }
        public BigInteger DefaultGasPrice { get; set; } = -1; // Setting the default gas price to -1 as a flag
        public abstract BigInteger DefaultGas { get; set; }
        public IAccount Account { get; protected set; }

#if !DOTNET35
        public abstract Task<string> SignTransactionAsync(TransactionInput transaction,
                                                          CancellationToken cancellationToken = default(CancellationToken));

        public Task<string> SendRawTransactionAsync(string signedTransaction,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Client == null) throw new NullReferenceException("Client not configured");
            if (string.IsNullOrEmpty(signedTransaction)) throw new ArgumentNullException(nameof(signedTransaction));
            var ethSendRawTransaction = new EthSendRawTransaction(Client);
            return ethSendRawTransaction.SendRequestAsync(signedTransaction, null, cancellationToken);
        }

        private ITransactionReceiptService _transactionReceiptService;
        public ITransactionReceiptService TransactionReceiptService {
            get
            {
                if (_transactionReceiptService == null) return TransactionReceiptServiceFactory.GetDefaultransactionReceiptService(this);
                return _transactionReceiptService;
            }
            set
            {
                _transactionReceiptService = value;
            }
        }

        public Task<TransactionReceipt> SendTransactionAndWaitForReceiptAsync(TransactionInput transactionInput, CancellationToken cancellationToken = default(CancellationToken))
        {
            return TransactionReceiptService.SendRequestAndWaitForReceiptAsync(transactionInput, cancellationToken);
        }
               
        public virtual Task<HexBigInteger> EstimateGasAsync(CallInput callInput,
                                                            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Client == null) throw new NullReferenceException("Client not configured");
            if (callInput == null) throw new ArgumentNullException(nameof(callInput));
            var ethEstimateGas = new EthEstimateGas(Client);
            return ethEstimateGas.SendRequestAsync(callInput, null, cancellationToken);
        }

        public abstract Task<string> SendTransactionAsync(TransactionInput transactionInput,
                                                          CancellationToken cancellationToken = default(CancellationToken));
        
        public virtual Task<string> SendTransactionAsync(string from, string to, HexBigInteger amount,
                                                         CancellationToken cancellationToken = default(CancellationToken))
        {  
            return SendTransactionAsync(new TransactionInput() { From = from, To = to, Value = amount},
                                        cancellationToken);
        }

        public async Task<HexBigInteger> GetGasPriceAsync(TransactionInput transactionInput,
                                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            if (transactionInput.GasPrice != null) return transactionInput.GasPrice;
            if (DefaultGasPrice >= 0) return new HexBigInteger(DefaultGasPrice);
            var ethGetGasPrice = new EthGasPrice(Client);
            return await ethGetGasPrice.SendRequestAsync(cancellationToken).ConfigureAwait(false);
        }

        protected void SetDefaultGasPriceAndCostIfNotSet(TransactionInput transactionInput)
        {
            if (DefaultGasPrice != -1)
            {
                if (transactionInput.GasPrice == null) transactionInput.GasPrice = new HexBigInteger(DefaultGasPrice);
            }

            if (DefaultGas != null)
            {
                if (transactionInput.Gas == null) transactionInput.Gas = new HexBigInteger(DefaultGas);
            }
        }
#endif
    }
}