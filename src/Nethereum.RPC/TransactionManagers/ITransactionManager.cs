﻿using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading;
using Nethereum.RPC.Accounts;
using Nethereum.RPC.TransactionReceipts;

namespace Nethereum.RPC.TransactionManagers
{
    public interface ITransactionManager
    {
        IClient Client { get; set; }
        BigInteger DefaultGasPrice { get; set; }
        BigInteger DefaultGas { get; set; }
        IAccount Account { get; }

#if !DOTNET35
        
        Task<string> SendTransactionAsync(TransactionInput transactionInput);
        Task<HexBigInteger> EstimateGasAsync(CallInput callInput);
        Task<string> SendTransactionAsync(string from, string to, HexBigInteger amount);
        Task<string> SignTransactionAsync(TransactionInput transaction);
        ITransactionReceiptService TransactionReceiptService { get; set; }
        Task<TransactionReceipt> SendTransactionAndWaitForReceiptAsync(TransactionInput transactionInput, CancellationTokenSource tokenSource);
#endif

    }
}
