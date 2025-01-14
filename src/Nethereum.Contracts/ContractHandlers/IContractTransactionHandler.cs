﻿using System.Threading;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Contracts.ContractHandlers
{
    public interface IContractTransactionHandler<TContractMessage> where TContractMessage : FunctionMessage, new()
    {
        Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(string contractAddress,
                                                                        TContractMessage functionMessage = null,
                                                                        CancellationToken cancellationToken = default(CancellationToken));

        Task<HexBigInteger> EstimateGasAsync(string contractAddress, TContractMessage functionMessage = null,
                                             CancellationToken cancellationToken = default(CancellationToken));

        Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(string contractAddress,
                                                                   TContractMessage functionMessage = null,
                                                                   CancellationToken cancellationToken = default(CancellationToken));

        Task<string> SendRequestAsync(string contractAddress,
                                      TContractMessage functionMessage = null,
                                      CancellationToken cancellationToken = default(CancellationToken));

        Task<string> SignTransactionAsync(string contractAddress,
                                          TContractMessage functionMessage = null,
                                          CancellationToken cancellationToken = default(CancellationToken));
    }
}