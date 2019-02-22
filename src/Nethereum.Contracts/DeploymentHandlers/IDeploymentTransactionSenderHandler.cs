using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.Contracts.DeploymentHandlers
{
    public interface IDeploymentTransactionSenderHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<string> SendTransactionAsync(TContractDeploymentMessage deploymentMessage = null,
                                          CancellationToken cancellationToken = default(CancellationToken));
    }
}