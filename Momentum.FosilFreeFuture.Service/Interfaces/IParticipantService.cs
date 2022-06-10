using System.Numerics;
using Benchain.FosilFreeFuture.Service.Models;
using Blockchain.Contracts.ProjectFunding_dev.ContractDefinition;
using Nethereum.Contracts;
using ProjectStartedEventDTO = Blockchain.Contracts.ProjectFunding_dev.ContractDefinition.ProjectStartedEventDTO;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IParticipantService
  {
    Task<BigInteger> GetCurrentBalance(string participantAddress);
    void GetBadges(string participantAddress);
    List<Object> GetProjects(string participantAddress);


  }
}