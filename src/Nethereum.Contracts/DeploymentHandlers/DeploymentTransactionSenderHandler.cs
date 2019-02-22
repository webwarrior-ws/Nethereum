using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.TransactionManagers;

namespace Nethereum.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public class DeploymentTransactionSenderHandler<TContractDeploymentMessage> : DeploymentHandlerBase<TContractDeploymentMessage>, 
        IDeploymentTransactionSenderHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        private IDeploymentEstimatorHandler<TContractDeploymentMessage> _deploymentEstimatorHandler;

        public DeploymentTransactionSenderHandler(ITransactionManager transactionManager):base(transactionManager)
        {
            _deploymentEstimatorHandler = new DeploymentEstimatorHandler<TContractDeploymentMessage>(transactionManager);
        }

        public async Task<string> SendTransactionAsync(TContractDeploymentMessage deploymentMessage = null,
                                                       CancellationToken cancellationToken = default(CancellationToken))
        {
            if(deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
            deploymentMessage.Gas = await GetOrEstimateMaximumGasAsync(deploymentMessage,
                                                                       cancellationToken).ConfigureAwait(false);
            var transactionInput = DeploymentMessageEncodingService.CreateTransactionInput(deploymentMessage);
            return await TransactionManager.SendTransactionAsync(transactionInput,
                                                                 cancellationToken).ConfigureAwait(false);
        }

        protected virtual async Task<HexBigInteger> GetOrEstimateMaximumGasAsync(
            TContractDeploymentMessage deploymentMessage,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return deploymentMessage.GetHexMaximumGas()
                   ?? await _deploymentEstimatorHandler.EstimateGasAsync(deploymentMessage,
                                                                         cancellationToken).ConfigureAwait(false);
        }
    }
#endif
}