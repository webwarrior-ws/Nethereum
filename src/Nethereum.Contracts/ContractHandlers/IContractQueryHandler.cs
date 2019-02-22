using System.Threading;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Contracts.ContractHandlers
{
    public interface IContractQueryHandler<TFunctionMessage> where TFunctionMessage : FunctionMessage, new()
    {
        Task<TFunctionOutput> QueryAsync<TFunctionOutput>(string contractAddress,
                                                          TFunctionMessage functionMessage = null,
                                                          BlockParameter block = null,
                                                          CancellationToken cancellationToken = default(CancellationToken));

        Task<TFunctionOutput> QueryDeserializingToObjectAsync<TFunctionOutput>(TFunctionMessage functionMessage,
                                                                               string contractAddress,
                                                                               BlockParameter block = null,
                                                                               CancellationToken cancellationToken = default(CancellationToken))
                                                                               where TFunctionOutput : IFunctionOutputDTO, new();

        Task<byte[]> QueryRawAsBytesAsync(string contractAddress,
                                          TFunctionMessage functionMessage = null,
                                          BlockParameter block = null,
                                          CancellationToken cancellationToken = default(CancellationToken));

        Task<string> QueryRawAsync(string contractAddress,
                                   TFunctionMessage functionMessage = null,
                                   BlockParameter block = null,
                                   CancellationToken cancellationToken = default(CancellationToken));
    }
}