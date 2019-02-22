using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.DeploymentHandlers;
using Nethereum.Contracts.Extensions;
using Nethereum.Hex.HexTypes;
using JsonRpcSharp.Client;
using Nethereum.RPC.Accounts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionManagers;

namespace Nethereum.Contracts.ContractHandlers
{
#if !DOTNET35
    public class ContractDeploymentTransactionHandler<TContractDeploymentMessage> : ContractTransactionHandlerBase, IContractDeploymentTransactionHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        private IDeploymentEstimatorHandler<TContractDeploymentMessage> _estimatorHandler;
        private IDeploymentTransactionReceiptPollHandler<TContractDeploymentMessage> _receiptPollHandler;
        private IDeploymentTransactionSenderHandler<TContractDeploymentMessage> _transactionSenderHandler;
        private IDeploymentSigner<TContractDeploymentMessage> _transactionSigner;
  
        public ContractDeploymentTransactionHandler(ITransactionManager transactionManager) : base(transactionManager)
        {
            _estimatorHandler = new DeploymentEstimatorHandler<TContractDeploymentMessage>(transactionManager);
            _receiptPollHandler = new DeploymentTransactionReceiptPollHandler<TContractDeploymentMessage>(transactionManager);
            _transactionSenderHandler = new DeploymentTransactionSenderHandler<TContractDeploymentMessage>(transactionManager);
            _transactionSigner = new DeploymentSigner<TContractDeploymentMessage>(transactionManager);
        }

        public Task<string> SignTransactionAsync(TContractDeploymentMessage contractDeploymentMessage,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            return _transactionSigner.SignTransactionAsync(contractDeploymentMessage, cancellationToken);
        }

        public Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(
            TContractDeploymentMessage contractDeploymentMessage = null, CancellationTokenSource tokenSource = null)
        {
            return _receiptPollHandler.SendTransactionAsync(contractDeploymentMessage, tokenSource);
        }

        public Task<string> SendRequestAsync(TContractDeploymentMessage contractDeploymentMessage = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return _transactionSenderHandler.SendTransactionAsync(contractDeploymentMessage,
                                                                  cancellationToken);
        }

        public Task<HexBigInteger> EstimateGasAsync(TContractDeploymentMessage contractDeploymentMessage,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return _estimatorHandler.EstimateGasAsync(contractDeploymentMessage,
                                                      cancellationToken);
        }

        public async Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(TContractDeploymentMessage deploymentMessage = null,
                                                                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            if (deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
            var gasEstimate = await EstimateGasAsync(deploymentMessage, cancellationToken)
                .ConfigureAwait(false);
            deploymentMessage.Gas = gasEstimate;
            return deploymentMessage.CreateTransactionInput();
        }
    }
#endif

}