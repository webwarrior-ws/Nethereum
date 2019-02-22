using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.Contracts.DeploymentHandlers
{
    public interface IDeploymentSigner<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<string> SignTransactionAsync(TContractDeploymentMessage deploymentMessage,
                                          CancellationToken cancellationToken = default(CancellationToken));
    }
}