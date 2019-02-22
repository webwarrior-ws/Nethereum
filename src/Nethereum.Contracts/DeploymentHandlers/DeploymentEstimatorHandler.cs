using System.Threading;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.TransactionManagers;

namespace Nethereum.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public class DeploymentEstimatorHandler<TContractDeploymentMessage> : DeploymentHandlerBase<TContractDeploymentMessage>, 
        IDeploymentEstimatorHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
    
        public DeploymentEstimatorHandler(ITransactionManager transactionManager):base(transactionManager)
        { 
        }


        public Task<HexBigInteger> EstimateGasAsync(TContractDeploymentMessage deploymentMessage = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            if(deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
            var callInput = DeploymentMessageEncodingService.CreateCallInput(deploymentMessage);
            return TransactionManager.EstimateGasAsync(callInput, cancellationToken);
        }
    }
#endif
}