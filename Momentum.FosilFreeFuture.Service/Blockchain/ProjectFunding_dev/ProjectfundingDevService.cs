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
using Blockchain.Contracts.ProjectFunding_dev.ContractDefinition;

namespace Blockchain.Contracts.ProjectFunding_dev
{
    public partial class ProjectfundingDevService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProjectfundingDevDeployment projectfundingDevDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectfundingDevDeployment>().SendRequestAndWaitForReceiptAsync(projectfundingDevDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProjectfundingDevDeployment projectfundingDevDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectfundingDevDeployment>().SendRequestAsync(projectfundingDevDeployment);
        }

        public static async Task<ProjectfundingDevService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProjectfundingDevDeployment projectfundingDevDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, projectfundingDevDeployment, cancellationTokenSource);
            return new ProjectfundingDevService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProjectfundingDevService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> CreateProjectRequestAsync(string title, string description, BigInteger amountToRaise)
        {
            var createProjectFunction = new CreateProjectFunction();
                createProjectFunction.Title = title;
                createProjectFunction.Description = description;
                createProjectFunction.AmountToRaise = amountToRaise;
            
             return ContractHandler.SendRequestAsync(createProjectFunction);
        }

        public Task<TransactionReceipt> CreateProjectRequestAndWaitForReceiptAsync(string title, string description, BigInteger amountToRaise, CancellationTokenSource cancellationToken = null)
        {
            var createProjectFunction = new CreateProjectFunction();
                createProjectFunction.Title = title;
                createProjectFunction.Description = description;
                createProjectFunction.AmountToRaise = amountToRaise;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createProjectFunction, cancellationToken);
        }

        public Task<string> ProjectsQueryAsync(ProjectsFunction projectsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectsFunction, string>(projectsFunction, blockParameter);
        }

        
        public Task<string> ProjectsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var projectsFunction = new ProjectsFunction();
                projectsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<ProjectsFunction, string>(projectsFunction, blockParameter);
        }
    }
}
