using System.Numerics;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace Benchain.FosilFreeFuture.Service
{
  public class ProjectSmartContractService : IProjectSmartContractService
  {
    private readonly IConfiguration _config;
    private const string address = "0x3581D61A8181bD531C013ddD4fC37D4E846f5e42"; //ProjectFunding.sol
    private const string ABI = @"[{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'contractAddress','type':'address'},{'indexed':false,'internalType':'address','name':'projectStarter','type':'address'},{'indexed':false,'internalType':'string','name':'projectTitle','type':'string'},{'indexed':false,'internalType':'string','name':'projectDesc','type':'string'},{'indexed':false,'internalType':'uint256','name':'goalAmount','type':'uint256'}],'name':'ProjectStarted','type':'event'},{'inputs':[{'internalType':'string','name':'title','type':'string'},{'internalType':'string','name':'description','type':'string'},{'internalType':'uint256','name':'amountToRaise','type':'uint256'}],'name':'createProject','outputs':[],'stateMutability':'nonpayable','type':'function'}]";

    public ProjectSmartContractService(IConfiguration configuration)
    {
      _config = configuration;
    }


    public string CreateProject()
    {
      var web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);
      var createProjectContract = web3.Eth.GetContract(ABI, address);

      var projectCreatorAccountAddress = "0xF1C8D9F04cADE0BF3Be8f8f4afD27497023Dd82A";
      var projectName = "Test Project";
      var projectDescription = "Test Description";
      var projectAmount = 50;

      try
      {
        var gas = new HexBigInteger(new BigInteger(400000));
        var value = new HexBigInteger(new BigInteger(0));

        var createProjectFunction = createProjectContract.GetFunction("createProject").SendTransactionAsync
          (projectCreatorAccountAddress, gas, value, projectName, projectDescription, projectAmount);

        createProjectFunction.Wait();

        return createProjectFunction.Result;
      }
      catch (Exception e)
      {
        return @$"Error: {0}, {e.Message})";
      }

    }

    public void GetProjects()
    {

    }

    public Contract? GetContract()
    {
      var web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);
      var contract = web3.Eth.GetContract(ABI, address);

      return contract;
    }

    public async Task<BigInteger> GetContractFunction(string functionName, Contract contract)
    {
      return await contract.GetFunction(functionName).CallAsync<BigInteger>();
    }

  }
}
