using System.Linq;
using System.Numerics;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Service.Models;
using Blockchain.Contracts.PartnerParticipantContract;
using Blockchain.Contracts.PartnerParticipantContract.ContractDefinition;
using Blockchain.Contracts.PartnerParticipantContractLedger;
using Blockchain.Contracts.PartnerParticipantContractLedger.ContractDefinition;
using Blockchain.Contracts.Project;
using Blockchain.Contracts.Project.ContractDefinition;
using Blockchain.Contracts.ProjectFunding;
using Blockchain.Contracts.ProjectFunding.ContractDefinition;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Benchain.FosilFreeFuture.Service
{
  public class PartnerService : IPartnerService
  {
    private readonly IConfiguration _config;
    private readonly Web3 web3;

    public PartnerService(IConfiguration configuration)
    {
      _config = configuration;
      web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);
    }

    public void SendCoinsToParticipant(string partnerAddress, string participantAddress, int amount)
    {
      try
      {
        string contractAddress = FindPartnerParticipantContractAddress(partnerAddress, participantAddress);

        if (contractAddress == null)
        {
          CreateNewContract(partnerAddress, participantAddress, amount);
        }
        else
        {
          AddCoinsToParticipant(contractAddress, partnerAddress, amount);
        }
      }
      catch (Exception e)
      {
        var test = 1;
      }
    }

    public int SpentAmount(string partnerAddress)
    {
      throw new NotImplementedException();
    }


    private void AddCoinsToParticipant(string contractAddress, string partnerAddress, int amount)
    { 
      PartnerParticipantContractService service = new PartnerParticipantContractService(web3, contractAddress);

      var addCoinsFunction = new AddCoinsFunction();
      addCoinsFunction.FromAddress = partnerAddress;
      addCoinsFunction.AmountToSend = new BigInteger(amount);

      var addCoinsRequest = service.AddCoinsRequestAsync(addCoinsFunction);
      addCoinsRequest.Wait();

      var test = 1;
    }

    private void CreateNewContract(string partnerAddress, string participantAddress, int amount)
    {
      PartnerParticipantContractLedgerService service = new PartnerParticipantContractLedgerService(web3, _config["BlockchainNetwork:TestPartnerParticipantContractLedgerAddress"]);

      var createContractFunction = new CreateContractFunction();
      createContractFunction.ParticipantAddress = participantAddress;
      createContractFunction.FromAddress = partnerAddress;
      createContractFunction.InitialAmount = new BigInteger(amount);

      var newContract = service.CreateContractRequestAsync(createContractFunction);
    }

    private string FindPartnerParticipantContractAddress(string partnerAddress, string participantAddress)
    {
      var ledgerContractAddress = _config["BlockchainNetwork:TestPartnerParticipantContractLedgerAddress"];
      PartnerParticipantContractLedgerService service = new PartnerParticipantContractLedgerService(web3, ledgerContractAddress);

      var eventHandler = web3.Eth.GetEvent<PartnerParticipantContractCreatedEventDTO>(ledgerContractAddress);

      var filterInput = eventHandler.CreateFilterInput(fromBlock: BlockParameter.CreateEarliest(), toBlock: BlockParameter.CreateLatest());
      var changes = eventHandler.GetAllChangesAsync(filterInput);

      changes.Wait();

      var result = "";

      foreach (var contractCreatedEvent in changes.Result) {
        if (contractCreatedEvent.Event.PartnerAddress == partnerAddress && contractCreatedEvent.Event.ParticipantAddress == participantAddress)
        {
          result = contractCreatedEvent.Event.ContractAddress;

          break;
        }
      }

      return result;
    }

  }
}