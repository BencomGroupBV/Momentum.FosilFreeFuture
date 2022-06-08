using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IProjectSmartContractService
  {
    string CreateProject();
    Task<BigInteger> GetContractFunction(string functionName, Contract contract);
    Task<HexBigInteger> GetProjects();
  }
}