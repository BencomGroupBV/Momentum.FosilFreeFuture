using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Blockchain.Contracts.Project_dev.ContractDefinition;

namespace Blockchain.Contracts.Project_dev
{
    public partial class ProjectDevService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProjectDevDeployment projectDevDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectDevDeployment>().SendRequestAndWaitForReceiptAsync(projectDevDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProjectDevDeployment projectDevDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectDevDeployment>().SendRequestAsync(projectDevDeployment);
        }

        public static async Task<ProjectDevService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProjectDevDeployment projectDevDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, projectDevDeployment, cancellationTokenSource);
            return new ProjectDevService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProjectDevService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> AmountGoalQueryAsync(AmountGoalFunction amountGoalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AmountGoalFunction, BigInteger>(amountGoalFunction, blockParameter);
        }

        
        public Task<BigInteger> AmountGoalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AmountGoalFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> CompleteAtQueryAsync(CompleteAtFunction completeAtFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CompleteAtFunction, BigInteger>(completeAtFunction, blockParameter);
        }

        
        public Task<BigInteger> CompleteAtQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CompleteAtFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> ContributionsQueryAsync(ContributionsFunction contributionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ContributionsFunction, BigInteger>(contributionsFunction, blockParameter);
        }

        
        public Task<BigInteger> ContributionsQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var contributionsFunction = new ContributionsFunction();
                contributionsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<ContributionsFunction, BigInteger>(contributionsFunction, blockParameter);
        }

        public Task<string> CreatorQueryAsync(CreatorFunction creatorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CreatorFunction, string>(creatorFunction, blockParameter);
        }

        
        public Task<string> CreatorQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CreatorFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> CurrentBalanceQueryAsync(CurrentBalanceFunction currentBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentBalanceFunction, BigInteger>(currentBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> CurrentBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> DescriptionQueryAsync(DescriptionFunction descriptionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(descriptionFunction, blockParameter);
        }

        
        public Task<string> DescriptionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> RaiseByQueryAsync(RaiseByFunction raiseByFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RaiseByFunction, BigInteger>(raiseByFunction, blockParameter);
        }

        
        public Task<BigInteger> RaiseByQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RaiseByFunction, BigInteger>(null, blockParameter);
        }

        public Task<byte> StateQueryAsync(StateFunction stateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StateFunction, byte>(stateFunction, blockParameter);
        }

        
        public Task<byte> StateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StateFunction, byte>(null, blockParameter);
        }

        public Task<string> TitleQueryAsync(TitleFunction titleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TitleFunction, string>(titleFunction, blockParameter);
        }

        
        public Task<string> TitleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TitleFunction, string>(null, blockParameter);
        }
    }
}
