using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.Contracts.TransactionHandlers
{
    public interface ITransactionSigner<TFunctionMessage> where TFunctionMessage : FunctionMessage, new()
    {
        Task<string> SignTransactionAsync(string contractAddress,
                                          TFunctionMessage functionMessage = null,
                                          CancellationToken cancellationToken = default(CancellationToken));
    }
}