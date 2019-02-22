using System;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts.Extensions;
using Nethereum.Contracts.TransactionHandlers;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionManagers;

namespace Nethereum.Contracts.ContractHandlers
{
#if !DOTNET35
    public class ContractTransactionHandler<TContractMessage> : ContractTransactionHandlerBase, IContractTransactionHandler<TContractMessage> where TContractMessage : FunctionMessage, new()
    {
        private ITransactionEstimatorHandler<TContractMessage> _estimatorHandler;
        private ITransactionReceiptPollHandler<TContractMessage> _receiptPollHandler;
        private ITransactionSenderHandler<TContractMessage> _transactionSenderHandler;
        private ITransactionSigner<TContractMessage> _transactionSigner;


        public ContractTransactionHandler(ITransactionManager transactionManager) : base(transactionManager)
        {
            _estimatorHandler = new TransactionEstimatorHandler<TContractMessage>(transactionManager);
            _receiptPollHandler = new TransactionReceiptPollHandler<TContractMessage>(transactionManager);
            _transactionSenderHandler = new TransactionSenderHandler<TContractMessage>(transactionManager);
            _transactionSigner = new TransactionSignerHandler<TContractMessage>(transactionManager);
        }

        public Task<string> SignTransactionAsync(
            string contractAddress,
            TContractMessage functionMessage = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _transactionSigner.SignTransactionAsync(contractAddress,
                                                           functionMessage,
                                                           cancellationToken);
        }

        public Task<TransactionReceipt> SendTransactionAndWaitForReceiptAsync(
            string contractAddress, TContractMessage functionMessage = null, CancellationTokenSource tokenSource = null)
        {
            return _receiptPollHandler.SendTransactionAsync(contractAddress, functionMessage, tokenSource);
        }

        [Obsolete("Use SendTransactionAndWaitForReceipt instead")]
        public Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(
            string contractAddress, TContractMessage functionMessage = null, CancellationTokenSource tokenSource = null)
        {
            return SendTransactionAndWaitForReceiptAsync(contractAddress, functionMessage, tokenSource);
        }

        public Task<string> SendTransactionAsync(string contractAddress,
                                                 TContractMessage functionMessage = null,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            return _transactionSenderHandler.SendTransactionAsync(contractAddress,
                                                                  functionMessage,
                                                                  cancellationToken);
        }

        [Obsolete("Use SendTransactionAsync instead")]
        public Task<string> SendRequestAsync(string contractAddress,
                                             TContractMessage functionMessage = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendTransactionAsync(contractAddress,
                                        functionMessage,
                                        cancellationToken);
        }

        public async Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(
            string contractAddress,
            TContractMessage functionMessage = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var gasEstimate = await EstimateGasAsync(contractAddress,
                                                     functionMessage,
                                                     cancellationToken)
                                                     .ConfigureAwait(false);
            functionMessage.Gas = gasEstimate;
            return functionMessage.CreateTransactionInput(contractAddress);
        }

        public Task<HexBigInteger> EstimateGasAsync(string contractAddress,
                                                    TContractMessage functionMessage = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return _estimatorHandler.EstimateGasAsync(contractAddress,
                                                      functionMessage,
                                                      cancellationToken);
        }
    }
#endif

}