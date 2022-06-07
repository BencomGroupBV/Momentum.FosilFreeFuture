using System.Numerics;
using Nethereum.Contracts;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IProjectSmartContractService
  {
    string CreateProject();
    Contract? GetContract();
    Task<BigInteger> GetContractFunction(string functionName, Contract contract);
    void GetProjects();
  }
}