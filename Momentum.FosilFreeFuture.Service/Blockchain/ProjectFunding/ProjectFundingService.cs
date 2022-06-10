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
using Blockchain.Contracts.ProjectFunding.ContractDefinition;

namespace Blockchain.Contracts.ProjectFunding
{
    public partial class ProjectFundingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProjectFundingDeployment projectFundingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectFundingDeployment>().SendRequestAndWaitForReceiptAsync(projectFundingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProjectFundingDeployment projectFundingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectFundingDeployment>().SendRequestAsync(projectFundingDeployment);
        }

        public static async Task<ProjectFundingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProjectFundingDeployment projectFundingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, projectFundingDeployment, cancellationTokenSource);
            return new ProjectFundingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProjectFundingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> CreateProjectRequestAsync(CreateProjectFunction createProjectFunction)
        {
             return ContractHandler.SendRequestAsync(createProjectFunction);
        }

        public Task<TransactionReceipt> CreateProjectRequestAndWaitForReceiptAsync(CreateProjectFunction createProjectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createProjectFunction, cancellationToken);
        }

        public Task<string> CreateProjectRequestAsync(ProjectDetails details, BigInteger fundsNeeded, string image, string logo, string initiated, string status)
        {
            var createProjectFunction = new CreateProjectFunction();
                createProjectFunction.Details = details;
                createProjectFunction.FundsNeeded = fundsNeeded;
                createProjectFunction.Image = image;
                createProjectFunction.Logo = logo;
                createProjectFunction.Initiated = initiated;
                createProjectFunction.Status = status;
            
             return ContractHandler.SendRequestAsync(createProjectFunction);
        }

        public Task<TransactionReceipt> CreateProjectRequestAndWaitForReceiptAsync(ProjectDetails details, BigInteger fundsNeeded, string image, string logo, string initiated, string status, CancellationTokenSource cancellationToken = null)
        {
            var createProjectFunction = new CreateProjectFunction();
                createProjectFunction.Details = details;
                createProjectFunction.FundsNeeded = fundsNeeded;
                createProjectFunction.Image = image;
                createProjectFunction.Logo = logo;
                createProjectFunction.Initiated = initiated;
                createProjectFunction.Status = status;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createProjectFunction, cancellationToken);
        }

        public Task<List<string>> ReturnAllProjectsQueryAsync(ReturnAllProjectsFunction returnAllProjectsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ReturnAllProjectsFunction, List<string>>(returnAllProjectsFunction, blockParameter);
        }

        
        public Task<List<string>> ReturnAllProjectsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ReturnAllProjectsFunction, List<string>>(null, blockParameter);
        }
    }
}
