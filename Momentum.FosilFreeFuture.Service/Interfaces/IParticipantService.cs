using System.Numerics;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IParticipantService
  {
    Task<BigInteger> GetCurrentBalance(string participantAddress);
    void GetBadges(string participantAddress);
    List<Object> GetProjects(string participantAddress);


  }
}