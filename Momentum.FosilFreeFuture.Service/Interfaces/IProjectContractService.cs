using System.Numerics;
using Nethereum.Contracts;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IProjectContractService
  {
    string CreateProject();
    Contract? GetContract();
    Task<BigInteger> GetContractFunction(string functionName, Contract contract);
    void GetProjects();
  }
}