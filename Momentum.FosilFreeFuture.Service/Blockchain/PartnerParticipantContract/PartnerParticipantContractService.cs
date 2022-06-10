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
using Blockchain.Contracts.PartnerParticipantContract.ContractDefinition;

namespace Blockchain.Contracts.PartnerParticipantContract
{
    public partial class PartnerParticipantContractService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, PartnerParticipantContractDeployment partnerParticipantContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<PartnerParticipantContractDeployment>().SendRequestAndWaitForReceiptAsync(partnerParticipantContractDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PartnerParticipantContractDeployment partnerParticipantContractDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<PartnerParticipantContractDeployment>().SendRequestAsync(partnerParticipantContractDeployment);
        }

        public static async Task<PartnerParticipantContractService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, PartnerParticipantContractDeployment partnerParticipantContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, partnerParticipantContractDeployment, cancellationTokenSource);
            return new PartnerParticipantContractService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public PartnerParticipantContractService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddCoinsRequestAsync(AddCoinsFunction addCoinsFunction)
        {
             return ContractHandler.SendRequestAsync(addCoinsFunction);
        }

        public Task<string> AddCoinsRequestAsync()
        {
             return ContractHandler.SendRequestAsync<AddCoinsFunction>();
        }

        public Task<TransactionReceipt> AddCoinsRequestAndWaitForReceiptAsync(AddCoinsFunction addCoinsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addCoinsFunction, cancellationToken);
        }

        public Task<TransactionReceipt> AddCoinsRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<AddCoinsFunction>(null, cancellationToken);
        }

        public Task<BigInteger> AmountOfCoinsQueryAsync(AmountOfCoinsFunction amountOfCoinsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AmountOfCoinsFunction, BigInteger>(amountOfCoinsFunction, blockParameter);
        }

        
        public Task<BigInteger> AmountOfCoinsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AmountOfCoinsFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> DonateRequestAsync(DonateFunction donateFunction)
        {
             return ContractHandler.SendRequestAsync(donateFunction);
        }

        public Task<TransactionReceipt> DonateRequestAndWaitForReceiptAsync(DonateFunction donateFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(donateFunction, cancellationToken);
        }

        public Task<string> DonateRequestAsync(string projectAddress)
        {
            var donateFunction = new DonateFunction();
                donateFunction.ProjectAddress = projectAddress;
            
             return ContractHandler.SendRequestAsync(donateFunction);
        }

        public Task<TransactionReceipt> DonateRequestAndWaitForReceiptAsync(string projectAddress, CancellationTokenSource cancellationToken = null)
        {
            var donateFunction = new DonateFunction();
                donateFunction.ProjectAddress = projectAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(donateFunction, cancellationToken);
        }

        public Task<GetContractDetailsOutputDTO> GetContractDetailsQueryAsync(GetContractDetailsFunction getContractDetailsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetContractDetailsFunction, GetContractDetailsOutputDTO>(getContractDetailsFunction, blockParameter);
        }

        public Task<GetContractDetailsOutputDTO> GetContractDetailsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetContractDetailsFunction, GetContractDetailsOutputDTO>(null, blockParameter);
        }

        public Task<string> ParticipantAddressQueryAsync(ParticipantAddressFunction participantAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ParticipantAddressFunction, string>(participantAddressFunction, blockParameter);
        }

        
        public Task<string> ParticipantAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ParticipantAddressFunction, string>(null, blockParameter);
        }

        public Task<string> PartnerAddressQueryAsync(PartnerAddressFunction partnerAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PartnerAddressFunction, string>(partnerAddressFunction, blockParameter);
        }

        
        public Task<string> PartnerAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PartnerAddressFunction, string>(null, blockParameter);
        }
    }
}
