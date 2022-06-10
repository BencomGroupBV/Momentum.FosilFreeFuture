using Blockchain.Contracts.Project.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchain.FosilFreeFuture.Service.Interfaces
{
  public interface IPartnerService
  {
    int SpentAmount(string partnerAddress);
    void SendCoinsToParticipant(string partnerAddress, string participantAddress, int amount);
  }
}
