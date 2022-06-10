using System.Numerics;
using Benchain.FosilFreeFuture.Service.Models;
using Blockchain.Contracts.PartnerParticipantContract.ContractDefinition;
using Blockchain.Contracts.Project.ContractDefinition;
using Blockchain.Contracts.ProjectFunding.ContractDefinition;
using Nethereum.Contracts;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IProjectSmartContractService
  {
    string CreateProject(ProjectModel projectModel);
    List<GetProjectDetailsOutputDTO> GetProjects();
    GetProjectDetailsOutputDTO GetProjectDetails(string projectAddress);
    List<GetProjectDetailsOutputDTO> GetProjectsSupportedByParticipant(string participantAddress);

    void ApproveProject(string projectAddress, string partnerAddress);
    Task<List<GetProjectDetailsOutputDTO>> GetApprovedProjects(string partnerAddress);
  }
}

