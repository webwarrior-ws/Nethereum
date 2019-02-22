using System.Threading;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Contracts.CQS
{
    public interface IContractDeploymentTransactionHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(TContractDeploymentMessage deploymentMessage = null,
                                                                        CancellationToken cancellationToken = default(CancellationToken));

        Task<HexBigInteger> EstimateGasAsync(TContractDeploymentMessage contractDeploymentMessage,
                                             CancellationToken cancellationToken = default(CancellationToken));

        Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(TContractDeploymentMessage contractDeploymentMessage = null,
                                                                   CancellationTokenSource tokenSource = null);

        Task<string> SendRequestAsync(TContractDeploymentMessage contractDeploymentMessage = null,
                                      CancellationToken cancellationToken = default(CancellationToken));

        Task<string> SignTransactionAsync(TContractDeploymentMessage contractDeploymentMessage,
                                          CancellationToken cancellationToken = default(CancellationToken));
    }
}