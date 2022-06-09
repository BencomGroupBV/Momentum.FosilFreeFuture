using System.Numerics;
using Benchain.FosilFreeFuture.Service.Models;
using Blockchain.Contracts.ProjectFunding_dev.ContractDefinition;
using Nethereum.Contracts;
using ProjectStartedEventDTO = Blockchain.Contracts.ProjectFunding_dev.ContractDefinition.ProjectStartedEventDTO;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IProjectSmartContractService
  {
    string CreateProject(ProjectModel projectModel);
    Task<BigInteger> GetContractFunction(string functionName, Contract contract);
    List<ProjectStartedEventDTO> GetProjects();
  }
}