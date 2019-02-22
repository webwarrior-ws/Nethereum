using System.Threading;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.QueryHandlers;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Contracts.ContractHandlers
{
#if !DOTNET35

    public abstract class ContractQueryHandlerBase<TFunctionMessage> : IContractQueryHandler<TFunctionMessage>
        where TFunctionMessage : FunctionMessage, new()
    {
        public Task<TFunctionOutput> QueryDeserializingToObjectAsync<TFunctionOutput>(
        TFunctionMessage functionMessage,
        string contractAddress,
        BlockParameter block = null,
        CancellationToken cancellationToken = default(CancellationToken))
            where TFunctionOutput : IFunctionOutputDTO, new()
        {
            var queryHandler = GetQueryDTOHandler<TFunctionOutput>();
            return queryHandler.QueryAsync(contractAddress, functionMessage, block, cancellationToken);
        }

        protected abstract QueryToDTOHandler<TFunctionMessage, TFunctionOutput> GetQueryDTOHandler<TFunctionOutput>() where TFunctionOutput : IFunctionOutputDTO, new();

        public Task<TFunctionOutput> QueryAsync<TFunctionOutput>(
            string contractAddress,
            TFunctionMessage functionMessage = null,
            BlockParameter block = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryHandler = GetQueryToSimpleTypeHandler<TFunctionOutput>();
            return queryHandler.QueryAsync(contractAddress, functionMessage, block, cancellationToken);
        }

        protected abstract QueryToSimpleTypeHandler<TFunctionMessage, TFunctionOutput> GetQueryToSimpleTypeHandler<TFunctionOutput>();

        public async Task<byte[]> QueryRawAsBytesAsync(
            string contractAddress,
            TFunctionMessage functionMessage = null,
            BlockParameter block = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var rawResult = await QueryRawAsync(contractAddress, functionMessage, block, cancellationToken)
                .ConfigureAwait(false);
            return rawResult.HexToByteArray();
        }

        public Task<string> QueryRawAsync(
            string contractAddress,
            TFunctionMessage functionMessage = null,
            BlockParameter block = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            QueryRawHandler<TFunctionMessage> queryHandler = GetQueryRawHandler();
            return queryHandler.QueryAsync(contractAddress, functionMessage, block, cancellationToken);
        }

        protected abstract QueryRawHandler<TFunctionMessage> GetQueryRawHandler();
    }
#endif
}