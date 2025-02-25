﻿using System.Threading;
using System.Threading.Tasks;
using Nethereum.ABI.Decoders;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Services;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Contracts.ContractHandlers
{
    public class ContractHandler
    {
        public ContractHandler(string contractAddress, EthApiContractService ethApiContractService,
            string addressFrom = null)
        {
            ContractAddress = contractAddress;
            EthApiContractService = ethApiContractService;
            AddressFrom = addressFrom;
        }

        protected string AddressFrom { get; set; }

        public string ContractAddress { get; }
        public EthApiContractService EthApiContractService { get; }

        public Event<TEventType> GetEvent<TEventType>() where TEventType : new()
        {
            if (!EventAttribute.IsEventType(typeof(TEventType))) return null;
            return new Event<TEventType>(EthApiContractService.Client, ContractAddress);
        }

        public Function<TEthereumContractFunctionMessage> GetFunction<TEthereumContractFunctionMessage>() where TEthereumContractFunctionMessage : new()
        {
            var contract = EthApiContractService.GetContract<TEthereumContractFunctionMessage>(ContractAddress);
            return contract.GetFunction<TEthereumContractFunctionMessage>();
        }

        protected void SetAddressFrom(ContractMessageBase contractMessage)
        {
            contractMessage.FromAddress = contractMessage.FromAddress ?? AddressFrom;
        }

#if !DOTNET35

        public Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync<TEthereumContractFunctionMessage>(
            TEthereumContractFunctionMessage transactionMesssage = null, CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
        {
            if (transactionMesssage == null) transactionMesssage = new TEthereumContractFunctionMessage();
            var command = EthApiContractService.GetContractTransactionHandler<TEthereumContractFunctionMessage>();
            SetAddressFrom(transactionMesssage);
            return command.SendRequestAndWaitForReceiptAsync(ContractAddress, transactionMesssage, cancellationToken);
        }
  
        public Task<string> SendRequestAsync<TEthereumContractFunctionMessage>(
            TEthereumContractFunctionMessage transactionMesssage = null,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
        {
            if(transactionMesssage == null) transactionMesssage = new TEthereumContractFunctionMessage();
            var command = EthApiContractService.GetContractTransactionHandler<TEthereumContractFunctionMessage>();
            SetAddressFrom(transactionMesssage);
            return command.SendRequestAsync(ContractAddress, transactionMesssage, cancellationToken);
        }

        public Task<string> SignTransactionAsync<TEthereumContractFunctionMessage>(
            TEthereumContractFunctionMessage transactionMesssage = null,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
        {
            if (transactionMesssage == null) transactionMesssage = new TEthereumContractFunctionMessage();
            var command = EthApiContractService.GetContractTransactionHandler<TEthereumContractFunctionMessage>();
            SetAddressFrom(transactionMesssage);
            return command.SignTransactionAsync(ContractAddress, transactionMesssage, cancellationToken);
        }

        public Task<HexBigInteger> EstimateGasAsync<TEthereumContractFunctionMessage>(
            TEthereumContractFunctionMessage transactionMesssage = null,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
        {
            if (transactionMesssage == null) transactionMesssage = new TEthereumContractFunctionMessage();
            var command = EthApiContractService.GetContractTransactionHandler<TEthereumContractFunctionMessage>();
            SetAddressFrom(transactionMesssage);
            return command.EstimateGasAsync(ContractAddress, transactionMesssage, cancellationToken);
        }

        public Task<TEthereumFunctionReturn> QueryDeserializingToObjectAsync<TEthereumContractFunctionMessage,
            TEthereumFunctionReturn>(TEthereumContractFunctionMessage ethereumContractFunctionMessage = null,
            BlockParameter blockParameter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
            where TEthereumFunctionReturn : IFunctionOutputDTO, new()
        {
            if (ethereumContractFunctionMessage == null) ethereumContractFunctionMessage = new TEthereumContractFunctionMessage();
            SetAddressFrom(ethereumContractFunctionMessage);
            var queryHandler = EthApiContractService.GetContractQueryHandler<TEthereumContractFunctionMessage>();
            return queryHandler.QueryDeserializingToObjectAsync<TEthereumFunctionReturn>(
                ethereumContractFunctionMessage,
                ContractAddress, blockParameter,
                cancellationToken);
        }

        public Task<TReturn> QueryAsync<TEthereumContractFunctionMessage, TReturn>(
            TEthereumContractFunctionMessage ethereumContractFunctionMessage = null, BlockParameter blockParameter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
        {
            if(ethereumContractFunctionMessage == null) ethereumContractFunctionMessage = new TEthereumContractFunctionMessage();
            SetAddressFrom(ethereumContractFunctionMessage);
            var queryHandler = EthApiContractService.GetContractQueryHandler<TEthereumContractFunctionMessage>();
            return queryHandler.QueryAsync<TReturn>(
                ContractAddress, ethereumContractFunctionMessage, blockParameter, cancellationToken);
        }

        public Task<byte[]> QueryRawAsync<TEthereumContractFunctionMessage>(TEthereumContractFunctionMessage ethereumContractFunctionMessage = null,
                                                                            BlockParameter blockParameter = null,
                                                                            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
        {
            if (ethereumContractFunctionMessage == null) ethereumContractFunctionMessage = new TEthereumContractFunctionMessage();
            SetAddressFrom(ethereumContractFunctionMessage);
            var queryHandler = EthApiContractService.GetContractQueryHandler<TEthereumContractFunctionMessage>();
            return queryHandler.QueryRawAsBytesAsync(
                ContractAddress, ethereumContractFunctionMessage, blockParameter, cancellationToken);
        }
        
        public Task<TReturn> QueryRawAsync<TEthereumContractFunctionMessage, TCustomDecoder, TReturn>(BlockParameter blockParameter = null,
                                                                                                      CancellationToken cancellationToken = default(CancellationToken))
           where TEthereumContractFunctionMessage : FunctionMessage, new()
           where TCustomDecoder : ICustomRawDecoder<TReturn>, new()
        {
            var ethereumContractFunctionMessage = new TEthereumContractFunctionMessage();
            return QueryRawAsync<TEthereumContractFunctionMessage, TCustomDecoder, TReturn>(ethereumContractFunctionMessage,
                                                                                            blockParameter,
                                                                                            cancellationToken);
        }

        public async Task<TReturn> QueryRawAsync<TEthereumContractFunctionMessage, TCustomDecoder, TReturn>(TEthereumContractFunctionMessage ethereumContractFunctionMessage,
                                                                                                            BlockParameter blockParameter = null,
                                                                                                            CancellationToken cancellationToken = default(CancellationToken))
            where TEthereumContractFunctionMessage : FunctionMessage, new()
            where TCustomDecoder : ICustomRawDecoder<TReturn>, new()
        {
          
            var result = await QueryRawAsync<TEthereumContractFunctionMessage>(ethereumContractFunctionMessage,
                blockParameter, cancellationToken).ConfigureAwait(false);
            var decoder = new TCustomDecoder();
            return decoder.Decode(result);
        }
#endif

    }

}