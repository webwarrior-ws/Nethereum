using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.ABI.Decoders;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts.CQS;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.StandardTokenEIP20.ContractDefinition;
using Nethereum.Web3;

namespace Nethereum.StandardTokenEIP20
{
    public class StandardTokenService
    {

        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Web3.Web3 web3, EIP20Deployment eIP20Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<EIP20Deployment>().SendRequestAndWaitForReceiptAsync(eIP20Deployment, cancellationTokenSource);
        }
        public static Task<string> DeployContractAsync(Web3.Web3 web3, EIP20Deployment eIP20Deployment,
                                                       CancellationToken cancellationToken = default(CancellationToken))
        {
            return web3.Eth.GetContractDeploymentHandler<EIP20Deployment>().SendRequestAsync(eIP20Deployment,
                                                                                             cancellationToken);
        }
        public static async Task<StandardTokenService> DeployContractAndGetServiceAsync(Web3.Web3 web3, EIP20Deployment eIP20Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, eIP20Deployment, cancellationTokenSource);
            return new StandardTokenService(web3, receipt.ContractAddress);
        }

        protected Web3.Web3 Web3 { get; set; }

        public StandardTokenService(Web3.Web3 web3, string address)
        {
            this.Web3 = web3;
            this.ContractHandler = web3.Eth.GetContractHandler(address);
        }

        public ContractHandler ContractHandler { get; }

        public Event<ApprovalEventDTO> GetApprovalEvent()
        {
            return ContractHandler.GetEvent<ApprovalEventDTO>();
        }

        public Event<TransferEventDTO> GetTransferEvent()
        { 
            return ContractHandler.GetEvent<TransferEventDTO>();
        }

        public Task<string> NameQueryAsync(NameFunction nameFunction = null,
                                           BlockParameter blockParameter = null,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryRawAsync<NameFunction, StringBytes32Decoder, string>(nameFunction,
                                                                                             blockParameter,
                                                                                             cancellationToken);
        }

        public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction = null,
                                             BlockParameter blockParameter = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryRawAsync<SymbolFunction, StringBytes32Decoder, string>(symbolFunction,
                                                                                               blockParameter,
                                                                                               cancellationToken);
        }

        public Task<string> ApproveRequestAsync(ApproveFunction approveFunction,
                                                CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.SendRequestAsync(approveFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<string> ApproveRequestAsync(string spender, BigInteger value,
                                                CancellationToken cancellationToken = default(CancellationToken))
        {
            var approveFunction = new ApproveFunction();
            approveFunction.Spender = spender;
            approveFunction.Value = value;

            return ContractHandler.SendRequestAsync(approveFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender,
                                                                             BigInteger value,
                                                                             CancellationTokenSource cancellationTokenSource = null)
        {
            var approveFunction = new ApproveFunction();
            approveFunction.Spender = spender;
            approveFunction.Value = value;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationTokenSource);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction,
                                                      BlockParameter blockParameter = null,
                                                      CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction,
                                                                               blockParameter, cancellationToken);
        }


        public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null,
                                                      CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null,
                                                                               blockParameter,
                                                                               cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction,
                                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.SendRequestAsync(transferFromFunction,
                                                    cancellationToken);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction,
                                                                                  CancellationTokenSource cancellationTokenSource = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationTokenSource);
        }

        public Task<string> TransferFromRequestAsync(string from, string to, BigInteger value,
                                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            var transferFromFunction = new TransferFromFunction();
            transferFromFunction.From = from;
            transferFromFunction.To = to;
            transferFromFunction.Value = value;

            return ContractHandler.SendRequestAsync(transferFromFunction,
                                                    cancellationToken);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from,
                                                                                  string to,
                                                                                  BigInteger value,
                                                                                  CancellationTokenSource cancellationTokenSource = null)
        {
            var transferFromFunction = new TransferFromFunction();
            transferFromFunction.From = from;
            transferFromFunction.To = to;
            transferFromFunction.Value = value;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationTokenSource);
        }

        public Task<BigInteger> BalancesQueryAsync(BalancesFunction balancesFunction,
                                                   BlockParameter blockParameter = null,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<BalancesFunction, BigInteger>(balancesFunction,
                                                                            blockParameter,
                                                                            cancellationToken);
        }

        public Task<BigInteger> BalancesQueryAsync(string address,
                                                   BlockParameter blockParameter = null,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            var balancesFunction = new BalancesFunction();
            balancesFunction.Address = address;

            return ContractHandler.QueryAsync<BalancesFunction, BigInteger>(balancesFunction,
                                                                            blockParameter,
                                                                            cancellationToken);
        }

        public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction,
                                             BlockParameter blockParameter = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction,
                                                                      blockParameter,
                                                                      cancellationToken);
        }

        public Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(null,
                                                                      blockParameter,
                                                                      cancellationToken);
        }

        public Task<BigInteger> AllowedQueryAsync(AllowedFunction allowedFunction,
                                                  BlockParameter blockParameter = null,
                                                  CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<AllowedFunction, BigInteger>(allowedFunction,
                                                                           blockParameter,
                                                                           cancellationToken);
        }

        public Task<BigInteger> AllowedQueryAsync(string owner,
                                                  string spender,
                                                  BlockParameter blockParameter = null,
                                                  CancellationToken cancellationToken = default(CancellationToken))
        {
            var allowedFunction = new AllowedFunction();
            allowedFunction.Owner = owner;
            allowedFunction.Spender = spender;

            return ContractHandler.QueryAsync<AllowedFunction, BigInteger>(allowedFunction,
                                                                           blockParameter,
                                                                           cancellationToken);
        }

        public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction,
                                                    BlockParameter blockParameter = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction,
                                                                             blockParameter,
                                                                             cancellationToken);
        }

        public Task<BigInteger> BalanceOfQueryAsync(string owner,
                                                    BlockParameter blockParameter = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            var balanceOfFunction = new BalanceOfFunction();
            balanceOfFunction.Owner = owner;

            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction,
                                                                             blockParameter,
                                                                             cancellationToken);
        }

        public Task<string> TransferRequestAsync(TransferFunction transferFunction,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.SendRequestAsync(transferFunction,
                                                    cancellationToken);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction,
                                                                              CancellationTokenSource cancellationTokenSource = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction,
                                                                     cancellationTokenSource);
        }

        public Task<string> TransferRequestAsync(string to,
                                                 BigInteger value,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            var transferFunction = new TransferFunction();
            transferFunction.To = to;
            transferFunction.Value = value;

            return ContractHandler.SendRequestAsync(transferFunction, cancellationToken);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string to,
                                                                              BigInteger value,
                                                                              CancellationTokenSource cancellationTokenSource = null)
        {
            var transferFunction = new TransferFunction();
            transferFunction.To = to;
            transferFunction.Value = value;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationTokenSource);
        }

        public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction,
                                                    BlockParameter blockParameter = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction,
                                                                             blockParameter,
                                                                             cancellationToken);
        }

        public Task<BigInteger> AllowanceQueryAsync(string owner,
                                                    string spender,
                                                    BlockParameter blockParameter = null,
                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            var allowanceFunction = new AllowanceFunction();
            allowanceFunction.Owner = owner;
            allowanceFunction.Spender = spender;

            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction,
                                                                             blockParameter,
                                                                             cancellationToken);
        }
    }
}
