using System.Numerics;
using Benchain.FosilFreeFuture.Service.Models;
using Nethereum.Contracts;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IProjectSmartContractService
  {
    string CreateProject();
    Task<BigInteger> GetContractFunction(string functionName, Contract contract);
    List<ProjectStartedEventDTO> GetProjects();
  }
}