using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Benchain.FosilFreeFuture.Service.Models
{
  [Event("ProjectStarted")]
  public class ProjectStartedEventDTO : IEventDTO
  {
    [Parameter("address", "contractAddress", 1, false)]
    public string ContractAddress { get; set; }

    [Parameter("address", "projectStarter", 2, false)]
    public string ProjectStarter { get; set; }

    [Parameter("string", "projectTitle", 3, false)]
    public string ProjectTitle { get; set; }

    [Parameter("string", "projectDesc", 4, false)]
    public string ProjectDescription { get; set; }

    [Parameter("uint256", "goalAmount", 5, false)]
    public BigInteger GoalAmount { get; set; }
  }
}
