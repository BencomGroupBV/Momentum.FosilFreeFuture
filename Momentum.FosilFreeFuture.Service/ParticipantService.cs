using Benchain.FosilFreeFuture.Service.Interfaces;
using Blockchain.Contracts.PartnerParticipantContractLedger;
using Blockchain.Contracts.PartnerParticipantContractLedger.ContractDefinition;
using Microsoft.Extensions.Configuration;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Benchain.FosilFreeFuture.Service
{
  internal class ParticipantService : IParticipantService
  {
    private readonly IConfiguration _config;
    private readonly Web3 web3;

    public ParticipantService(IConfiguration configuration)
    {
      _config = configuration;
      web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);
    }

    public void GetBadges(string participantAddress)
    {
      throw new NotImplementedException();
    }

    public int GetPortfolio()
    {
      // simply sum over the results in GerCurrentBalance()
      throw new NotImplementedException();
    }

    public List<Object> GetCurrentBalance(string participantAddress)
    {
      /*
      List<string> contractAddresses = FindParticipantContractAddresses(participantAddress);

      List<>

      foreach (var contractaddress in contractAddresses)
      {
        // get contract.amountOfCoins;
        PartnerParticipantContractLedgerService service = new PartnerParticipantContractLedgerService(web3, contractAddress);
        AmountOfCoinsQueryAsync query = new AmountOfCoinsQueryAsync(null);
        var coins = service.AmountOfCoinsQueryAsync(query);
        coins.Wait();
      }
      */

      throw new NotImplementedException();
    }


    public List<object> GetProjects(string participantAddress)
    {
      throw new NotImplementedException();
    }

    private List<string> FindParticipantContractAddresses(string participantAddress)
    {
      var ledgerContractAddress = _config["Blockchain:TestPartnerParticipantContractLedgerAddress"];
      PartnerParticipantContractLedgerService service = new PartnerParticipantContractLedgerService(web3, ledgerContractAddress);

      var eventHandler = web3.Eth.GetEvent<PartnerParticipantContractCreatedEventDTO>(ledgerContractAddress);

      var filterInput = eventHandler.CreateFilterInput(fromBlock: BlockParameter.CreateEarliest(), toBlock: BlockParameter.CreateLatest());
      var changes = eventHandler.GetAllChangesAsync(filterInput);

      changes.Wait();

      var result = new List<string>();

      foreach (var contractCreatedEvent in changes.Result)
      {
        if (contractCreatedEvent.Event.ParticipantAddress == participantAddress)
        {
          result.Add(contractCreatedEvent.Event.ContractAddress);

          break;
        }
      }

      return result;
    }

    Task<BigInteger> IParticipantService.GetCurrentBalance(string participantAddress)
    {
      throw new NotImplementedException();
    }
  }
}
