using System.Numerics;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Service.Models;
using Blockchain.Contracts.PartnerParticipantContract.ContractDefinition;
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
  public class ProjectSmartContractService : IProjectSmartContractService
  {
    private readonly IConfiguration _config;
    private readonly Web3 web3;

    public ProjectSmartContractService(IConfiguration configuration)
    {
      _config = configuration;
      web3 = new Web3(_config["BlockchainNetwork:EndPoint"]);
    }

    public string CreateProject(ProjectModel projectModel)
    {
      try
      {
        projectModel.InitiatorWalletAddress = _config["BlockchainNetwork:TestInitiatorAddress"];
        var contractAddress = _config["BlockchainNetwork:TestProjectFundingContractAddress"];

        var service = new ProjectFundingService(web3, contractAddress);

        var createProjectFunction = new CreateProjectFunction();
        createProjectFunction.FromAddress = projectModel.InitiatorWalletAddress;

        createProjectFunction.Details = new ProjectDetails();
        createProjectFunction.Details.Name = projectModel.Name;
        createProjectFunction.Details.Description = projectModel.Description;
        createProjectFunction.Details.Country = projectModel.Country;

        createProjectFunction.FundsNeeded = new BigInteger(projectModel.FundsNeeded);
        createProjectFunction.Image = projectModel.Image == null ? "" : projectModel.Image;
        createProjectFunction.Logo = projectModel.Logo == null ? "" : projectModel.Logo;
        createProjectFunction.Initiated = projectModel.InitiatorWalletAddress;
        createProjectFunction.Status = "Open";

        var projectAddress = service.CreateProjectRequestAsync(createProjectFunction);
        projectAddress.Wait();

        return projectAddress.Result;
      }
      catch (Exception e)
      {
        return @$"Error: {e.Message})";
      }
    }

    public List<GetProjectDetailsOutputDTO> GetProjects()
    {
     
      var projectAddresses = new List<string>();

      var contractAddress = _config["BlockchainNetwork:TestProjectFundingContractAddress"];
      var eventHandler = web3.Eth.GetEvent<ProjectCreatedEventDTO>(contractAddress);

      var filterInput = eventHandler.CreateFilterInput(fromBlock: BlockParameter.CreateEarliest(), toBlock: BlockParameter.CreateLatest());
      var changes = eventHandler.GetAllChangesAsync(filterInput);

      changes.Wait();

      changes.Result.ForEach(result => projectAddresses.Add(result.Event.ContractAddress));

      return GetDetails(projectAddresses);
    }

    public List<GetProjectDetailsOutputDTO> GetProjectsSupportedByParticipant(string participantAddress)
    {
      var projectDTOs = new List<ParticipantDonatedEventDTO>();
      var ledgerContractAddress = _config["BlockchainNetwork:TestPartnerParticipantContractLedgerAddress"];

      var eventHandler = web3.Eth.GetEvent<ParticipantDonatedEventDTO>(ledgerContractAddress);

      var filterInput = eventHandler.CreateFilterInput(participantAddress, fromBlock: BlockParameter.CreateEarliest(), toBlock: BlockParameter.CreateLatest());
      var changes = eventHandler.GetAllChangesAsync(filterInput);

      changes.Wait();

      changes.Result.ForEach(result => projectDTOs.Add(result.Event));


      var results = new List<GetProjectDetailsOutputDTO>();

      foreach (var projectDTO in projectDTOs)
      {
        results.Add(GetProjectDetails(projectDTO.ProjectAddress));
      }

      return results;
    }

    public void ApproveProject(string projectAddress, string partnerAddress)
    {
      var projectService = new ProjectService(web3, projectAddress);

      var approveProjectFunction = new ApproveProjectFunction();
      approveProjectFunction.Gas = new BigInteger(1000000);
      approveProjectFunction.FromAddress = partnerAddress;

      var result = projectService.ApproveProjectRequestAsync(approveProjectFunction);
      result.Wait();
    }

    public List<ProjectApprovedEventDTO> GetApprovedProjects(string partnerAddress)
    {
      throw new NotImplementedException();
      /*
      var allProjects = GetProjects();

      var approvedProjects = new List<ProjectApprovedEventDTO>();

      foreach (var project in allProjects) {
        var results = new List<ProjectApprovedEventDTO>();

        // var projectAddress = project.Event.ContractAddress;
        var eventHandler = web3.Eth.GetEvent<ProjectApprovedEventDTO>(projectAddress);

        var filterInput = eventHandler.CreateFilterInput(partnerAddress, fromBlock: BlockParameter.CreateEarliest(), toBlock: BlockParameter.CreateLatest());
        var changes = eventHandler.GetAllChangesAsync(filterInput);

        changes.Wait();

        changes.Result.ForEach(result => approvedProjects.Add(result.Event));
      }

      return approvedProjects;*/
    }

    public GetProjectDetailsOutputDTO GetProjectDetails(string projectAddress)
    {
      var projectService = new ProjectService(web3, projectAddress);

      var getProjectDetailsFunction = new GetProjectDetailsFunction();
      var projectDetails = projectService.GetProjectDetailsQueryAsync(getProjectDetailsFunction, null);
      projectDetails.Wait();

      return projectDetails.Result;
    }

    private List<GetProjectDetailsOutputDTO> GetDetails(List<string> projectAddresses)
    {
      var result = new List<GetProjectDetailsOutputDTO>();

      foreach (var projectAddress in projectAddresses)
      {
        result.Add(GetProjectDetails(projectAddress));
      }

      return result;
    }

    List<GetProjectDetailsOutputDTO> IProjectSmartContractService.GetApprovedProjects(string partnerAddress)
    {
      throw new NotImplementedException();
    }

    /*
    private string FindProjectsForPartner(string partnerAddress)
    {
      var ledgerContractAddress = _config["BlockchainNetwork:TestPartnerParticipantContractLedgerAddress"];
      PartnerParticipantContractLedgerService service = new PartnerParticipantContractLedgerService(web3, ledgerContractAddress);

      var eventHandler = web3.Eth.GetEvent<PartnerParticipantContractCreatedEventDTO>(ledgerContractAddress);

      var filterInput = eventHandler.CreateFilterInput(fromBlock: BlockParameter.CreateEarliest(), toBlock: BlockParameter.CreateLatest());
      var changes = eventHandler.GetAllChangesAsync(filterInput);

      changes.Wait();

      var result = "";

      foreach (var contractCreatedEvent in changes.Result)
      {
        if (contractCreatedEvent.Event.PartnerAddress == partnerAddress && contractCreatedEvent.Event.ParticipantAddress == participantAddress)
        {
          result = contractCreatedEvent.Event.ContractAddress;

          break;
        }
      }

      return result;
    }
    */


  }
}
