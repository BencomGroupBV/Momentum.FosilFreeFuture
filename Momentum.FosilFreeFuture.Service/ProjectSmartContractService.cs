using System.Numerics;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Service.Models;
using Blockchain.Contracts.Project;
using Blockchain.Contracts.Project.ContractDefinition;
using Blockchain.Contracts.Project_dev.ContractDefinition;
using Blockchain.Contracts.ProjectFunding_dev;
using Blockchain.Contracts.ProjectFunding_dev.ContractDefinition;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using ProjectStartedEventDTO = Blockchain.Contracts.ProjectFunding_dev.ContractDefinition.ProjectStartedEventDTO;

namespace Benchain.FosilFreeFuture.Service
{
  public class ProjectSmartContractService : IProjectSmartContractService
  {
    private readonly IConfiguration _config;
    private const string contractAddress = "0x56458410430024cbAB857eA36E596849ab46c979"; //ProjectFunding_dev.sol
    private const string ABI = @"[{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'contractAddress','type':'address'},{'indexed':false,'internalType':'address','name':'projectStarter','type':'address'},{'indexed':false,'internalType':'string','name':'projectTitle','type':'string'},{'indexed':false,'internalType':'string','name':'projectDesc','type':'string'},{'indexed':false,'internalType':'uint256','name':'goalAmount','type':'uint256'}],'name':'ProjectStarted','type':'event'},{'inputs':[{'internalType':'uint256','name':'','type':'uint256'}],'name':'projects','outputs':[{'internalType':'contract Project_dev','name':'','type':'address'}],'stateMutability':'view','type':'function','constant':true},{'inputs':[{'internalType':'string','name':'title','type':'string'},{'internalType':'string','name':'description','type':'string'},{'internalType':'uint256','name':'amountToRaise','type':'uint256'}],'name':'createProject','outputs':[],'stateMutability':'nonpayable','type':'function'}]";


    public ProjectSmartContractService(IConfiguration configuration)
    {
      _config = configuration;
    }

    private Contract GetProjectSmartContract()
    {
      var web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);
      var contract = web3.Eth.GetContract(ABI, contractAddress);

      return contract;
    }

    public string CreateProject(ProjectModel projectModel)
    {
      try
      {
        projectModel.InitiatorWalletAddress = "0x4487e255846EbD79AB36c7fDc249B080Eb9F6953";

        var web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);

        var service = new ProjectfundingDevService(web3, contractAddress);

        var createProjectFunction = new CreateProjectFunction();
        createProjectFunction.Description = projectModel.Description;
        createProjectFunction.FromAddress = projectModel.InitiatorWalletAddress;
        createProjectFunction.Title = projectModel.Name;
        createProjectFunction.AmountToRaise = new BigInteger(projectModel.FundsNeeded);

        var projectAddress = service.CreateProjectRequestAsync(createProjectFunction);
        projectAddress.Wait();

        return projectAddress.Result; // this is the tx hash      
      }
      catch (Exception e)
      {
        return @$"Error: {e.Message})";
      }

    }

    public List<ProjectStartedEventDTO> GetProjects()
    {
      var createProjectSmartContract = GetProjectSmartContract();

      var eventLog = createProjectSmartContract.GetEvent("ProjectStarted");
      var filterInput = eventLog.CreateFilterInput(BlockParameter.CreateEarliest(), BlockParameter.CreateLatest());
      var logs = eventLog.GetAllChangesAsync<ProjectStartedEventDTO>(filterInput);

      logs.Wait();

      var results = new List<ProjectStartedEventDTO>();
      logs.Result.ForEach(result => results.Add(result.Event));

      return results;
    }

    public async Task<BigInteger> GetContractFunction(string functionName, Contract contract)
    {
      return await contract.GetFunction(functionName).CallAsync<BigInteger>();
    }
  }
}
