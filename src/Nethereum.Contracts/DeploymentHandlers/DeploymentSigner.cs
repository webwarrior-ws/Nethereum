using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.TransactionManagers;

namespace Nethereum.Contracts.DeploymentHandlers
{
#if !DOTNET35
    /// <summary>
    /// Signs a transaction estimating the gas if not set and retrieving the next nonce if not set
    /// </summary>
    public class DeploymentSigner<TContractDeploymentMessage> : DeploymentHandlerBase<TContractDeploymentMessage>, 
        IDeploymentSigner<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        private IDeploymentEstimatorHandler<TContractDeploymentMessage> _deploymentEstimatorHandler;
        private ITransactionManager transactionManager;

       
        public DeploymentSigner(ITransactionManager transactionManager) : this(transactionManager,
            new DeploymentEstimatorHandler<TContractDeploymentMessage>(transactionManager))
        {

        }

        public DeploymentSigner(ITransactionManager transactionManager, 
            IDeploymentEstimatorHandler<TContractDeploymentMessage> deploymentEstimatorHandler) : base(transactionManager)  
        {
            _deploymentEstimatorHandler = deploymentEstimatorHandler;
        }

        public async Task<string> SignTransactionAsync(TContractDeploymentMessage deploymentMessage = null,
                                                       CancellationToken cancellationToken = default(CancellationToken))
        {
            if (deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
            deploymentMessage.Gas = await GetOrEstimateMaximumGasAsync(deploymentMessage,
                                                                       cancellationToken).ConfigureAwait(false);
            var transactionInput = DeploymentMessageEncodingService.CreateTransactionInput(deploymentMessage);
            return await TransactionManager.SignTransactionAsync(transactionInput,
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