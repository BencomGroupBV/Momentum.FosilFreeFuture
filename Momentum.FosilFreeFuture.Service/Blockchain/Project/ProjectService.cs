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
using Blockchain.Contracts.Project.ContractDefinition;

namespace Blockchain.Contracts.Project
{
    public partial class ProjectService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProjectDeployment projectDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectDeployment>().SendRequestAndWaitForReceiptAsync(projectDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProjectDeployment projectDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProjectDeployment>().SendRequestAsync(projectDeployment);
        }

        public static async Task<ProjectService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProjectDeployment projectDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, projectDeployment, cancellationTokenSource);
            return new ProjectService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProjectService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<GetProjectDetailsOutputDTO> GetProjectDetailsQueryAsync(GetProjectDetailsFunction getProjectDetailsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectDetailsFunction, GetProjectDetailsOutputDTO>(getProjectDetailsFunction, blockParameter);
        }

        public Task<GetProjectDetailsOutputDTO> GetProjectDetailsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectDetailsFunction, GetProjectDetailsOutputDTO>(null, blockParameter);
        }

        public Task<string> ApproveProjectRequestAsync(ApproveProjectFunction approveProjectFunction)
        {
             return ContractHandler.SendRequestAsync(approveProjectFunction);
        }

        public Task<string> ApproveProjectRequestAsync()
        {
             return ContractHandler.SendRequestAsync<ApproveProjectFunction>();
        }

        public Task<TransactionReceipt> ApproveProjectRequestAndWaitForReceiptAsync(ApproveProjectFunction approveProjectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveProjectFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ApproveProjectRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<ApproveProjectFunction>(null, cancellationToken);
        }

        public Task<string> CountryQueryAsync(CountryFunction countryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CountryFunction, string>(countryFunction, blockParameter);
        }

        
        public Task<string> CountryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CountryFunction, string>(null, blockParameter);
        }

        public Task<string> DescriptionQueryAsync(DescriptionFunction descriptionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(descriptionFunction, blockParameter);
        }

        
        public Task<string> DescriptionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> FundsNeededQueryAsync(FundsNeededFunction fundsNeededFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundsNeededFunction, BigInteger>(fundsNeededFunction, blockParameter);
        }

        
        public Task<BigInteger> FundsNeededQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundsNeededFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> FundsReceivedQueryAsync(FundsReceivedFunction fundsReceivedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundsReceivedFunction, BigInteger>(fundsReceivedFunction, blockParameter);
        }

        
        public Task<BigInteger> FundsReceivedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundsReceivedFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ImageQueryAsync(ImageFunction imageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ImageFunction, string>(imageFunction, blockParameter);
        }

        
        public Task<string> ImageQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ImageFunction, string>(null, blockParameter);
        }

        public Task<string> InitiatedQueryAsync(InitiatedFunction initiatedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<InitiatedFunction, string>(initiatedFunction, blockParameter);
        }

        
        public Task<string> InitiatedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<InitiatedFunction, string>(null, blockParameter);
        }

        public Task<string> LogoQueryAsync(LogoFunction logoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LogoFunction, string>(logoFunction, blockParameter);
        }

        
        public Task<string> LogoQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LogoFunction, string>(null, blockParameter);
        }

        public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
        }

        
        public Task<string> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
        }

        public Task<string> StatusQueryAsync(StatusFunction statusFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StatusFunction, string>(statusFunction, blockParameter);
        }

        
        public Task<string> StatusQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StatusFunction, string>(null, blockParameter);
        }
    }
}
