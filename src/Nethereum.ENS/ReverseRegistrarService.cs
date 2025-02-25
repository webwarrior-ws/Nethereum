﻿using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.ENS.ReverseRegistrar.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.ENS
{

    public partial class ReverseRegistrarService
    {
    
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ReverseRegistrarDeployment reverseRegistrarDeployment, CancellationToken cancellationToken = default(CancellationToken))
        {
            return web3.Eth.GetContractDeploymentHandler<ReverseRegistrarDeployment>().SendRequestAndWaitForReceiptAsync(reverseRegistrarDeployment, cancellationToken);
        }
        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ReverseRegistrarDeployment reverseRegistrarDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ReverseRegistrarDeployment>().SendRequestAsync(reverseRegistrarDeployment);
        }
        public static async Task<ReverseRegistrarService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ReverseRegistrarDeployment reverseRegistrarDeployment, CancellationToken cancellationToken = default(CancellationToken))
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, reverseRegistrarDeployment, cancellationToken);
            return new ReverseRegistrarService(web3, receipt.ContractAddress);
        }
    
        protected Nethereum.Web3.Web3 Web3{ get; }
        
        public ContractHandler ContractHandler { get; }
        
        public ReverseRegistrarService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }
    
        public Task<string> ClaimWithResolverRequestAsync(ClaimWithResolverFunction claimWithResolverFunction)
        {
             return ContractHandler.SendRequestAsync(claimWithResolverFunction);
        }

        public Task<TransactionReceipt> ClaimWithResolverRequestAndWaitForReceiptAsync(ClaimWithResolverFunction claimWithResolverFunction, CancellationToken cancellationToken = default(CancellationToken))
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimWithResolverFunction, cancellationToken);
        }

        public Task<string> ClaimWithResolverRequestAsync(string owner, string resolver)
        {
            var claimWithResolverFunction = new ClaimWithResolverFunction();
                claimWithResolverFunction.Owner = owner;
                claimWithResolverFunction.Resolver = resolver;
            
             return ContractHandler.SendRequestAsync(claimWithResolverFunction);
        }

        public Task<TransactionReceipt> ClaimWithResolverRequestAndWaitForReceiptAsync(string owner, string resolver, CancellationToken cancellationToken = default(CancellationToken))
        {
            var claimWithResolverFunction = new ClaimWithResolverFunction();
                claimWithResolverFunction.Owner = owner;
                claimWithResolverFunction.Resolver = resolver;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimWithResolverFunction, cancellationToken);
        }

        public Task<string> ClaimRequestAsync(ClaimFunction claimFunction)
        {
             return ContractHandler.SendRequestAsync(claimFunction);
        }

        public Task<TransactionReceipt> ClaimRequestAndWaitForReceiptAsync(ClaimFunction claimFunction, CancellationToken cancellationToken = default(CancellationToken))
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimFunction, cancellationToken);
        }

        public Task<string> ClaimRequestAsync(string owner)
        {
            var claimFunction = new ClaimFunction();
                claimFunction.Owner = owner;
            
             return ContractHandler.SendRequestAsync(claimFunction);
        }

        public Task<TransactionReceipt> ClaimRequestAndWaitForReceiptAsync(string owner, CancellationToken cancellationToken = default(CancellationToken))
        {
            var claimFunction = new ClaimFunction();
                claimFunction.Owner = owner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimFunction, cancellationToken);
        }

        public Task<string> EnsQueryAsync(EnsFunction ensFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EnsFunction, string>(ensFunction, blockParameter);
        }

        
        public Task<string> EnsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EnsFunction, string>(null, blockParameter);
        }



        public Task<string> DefaultResolverQueryAsync(DefaultResolverFunction defaultResolverFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultResolverFunction, string>(defaultResolverFunction, blockParameter);
        }

        
        public Task<string> DefaultResolverQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultResolverFunction, string>(null, blockParameter);
        }



        public Task<byte[]> NodeQueryAsync(NodeFunction nodeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NodeFunction, byte[]>(nodeFunction, blockParameter);
        }

        
        public Task<byte[]> NodeQueryAsync(string addr, BlockParameter blockParameter = null)
        {
            var nodeFunction = new NodeFunction();
                nodeFunction.Addr = addr;
            
            return ContractHandler.QueryAsync<NodeFunction, byte[]>(nodeFunction, blockParameter);
        }



        public Task<string> SetNameRequestAsync(SetNameFunction setNameFunction)
        {
             return ContractHandler.SendRequestAsync(setNameFunction);
        }

        public Task<TransactionReceipt> SetNameRequestAndWaitForReceiptAsync(SetNameFunction setNameFunction, CancellationToken cancellationToken = default(CancellationToken))
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setNameFunction, cancellationToken);
        }

        public Task<string> SetNameRequestAsync(string name)
        {
            var setNameFunction = new SetNameFunction();
                setNameFunction.Name = name;
            
             return ContractHandler.SendRequestAsync(setNameFunction);
        }

        public Task<TransactionReceipt> SetNameRequestAndWaitForReceiptAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var setNameFunction = new SetNameFunction();
                setNameFunction.Name = name;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setNameFunction, cancellationToken);
        }
    }
}
