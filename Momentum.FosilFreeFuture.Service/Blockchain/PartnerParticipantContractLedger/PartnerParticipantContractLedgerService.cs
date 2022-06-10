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
using Blockchain.Contracts.PartnerParticipantContractLedger.ContractDefinition;

namespace Blockchain.Contracts.PartnerParticipantContractLedger
{
    public partial class PartnerParticipantContractLedgerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, PartnerParticipantContractLedgerDeployment partnerParticipantContractLedgerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<PartnerParticipantContractLedgerDeployment>().SendRequestAndWaitForReceiptAsync(partnerParticipantContractLedgerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PartnerParticipantContractLedgerDeployment partnerParticipantContractLedgerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<PartnerParticipantContractLedgerDeployment>().SendRequestAsync(partnerParticipantContractLedgerDeployment);
        }

        public static async Task<PartnerParticipantContractLedgerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, PartnerParticipantContractLedgerDeployment partnerParticipantContractLedgerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, partnerParticipantContractLedgerDeployment, cancellationTokenSource);
            return new PartnerParticipantContractLedgerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public PartnerParticipantContractLedgerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> CreateContractRequestAsync(CreateContractFunction createContractFunction)
        {
             return ContractHandler.SendRequestAsync(createContractFunction);
        }

        public Task<TransactionReceipt> CreateContractRequestAndWaitForReceiptAsync(CreateContractFunction createContractFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createContractFunction, cancellationToken);
        }

        public Task<string> CreateContractRequestAsync(string participantAddress, BigInteger initialAmount)
        {
            var createContractFunction = new CreateContractFunction();
                createContractFunction.ParticipantAddress = participantAddress;
                createContractFunction.InitialAmount = initialAmount;
            
             return ContractHandler.SendRequestAsync(createContractFunction);
        }

        public Task<TransactionReceipt> CreateContractRequestAndWaitForReceiptAsync(string participantAddress, BigInteger initialAmount, CancellationTokenSource cancellationToken = null)
        {
            var createContractFunction = new CreateContractFunction();
                createContractFunction.ParticipantAddress = participantAddress;
                createContractFunction.InitialAmount = initialAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createContractFunction, cancellationToken);
        }
    }
}
