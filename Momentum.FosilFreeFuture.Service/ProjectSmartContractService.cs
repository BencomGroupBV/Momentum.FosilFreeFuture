using System.Numerics;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Service.Models;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Benchain.FosilFreeFuture.Service
{
  public class ProjectSmartContractService : IProjectSmartContractService
  {
    private readonly IConfiguration _config;
    private const string contractAddress = "0x235A3a59748D5fc4FC7af5eC50414f36dc81ADfD"; //ProjectFunding_dev.sol
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
      var createProjectSmartContract = GetProjectSmartContract();
      projectModel.InitiatorWalletAddress = "0xf452e0260223A5ecbA2DCB476030a5b78818e2ED";

      try
      {
        var gas = new HexBigInteger(new BigInteger(700000));
        var value = new HexBigInteger(new BigInteger(0));

        var createProjectFunction = createProjectSmartContract.GetFunction("createProject").SendTransactionAsync
          (projectModel.InitiatorWalletAddress, gas, value, projectModel.Name, projectModel.Description, projectModel.FundsNeeded);

        createProjectFunction.Wait();

        var test = createProjectFunction.Result;

        return createProjectFunction.Result;
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
