namespace Benchain.FosilFreeFuture.Service.Models
{
    public class ProjectModel
    {
      public string Id { get; set; }
      public string Name { get; set; }
      public string Country { get; set; }
      public string Description { get; set; }
      public string Logo { get; set; }
      public string Image { get; set; }
      public string InitiatorWalletAddress { get; set; }
      public int FundsNeeded { get; set; }
      public int FundsReceived { get; set; }
      public string Status { get; set; }
      public int ParticipantId { get; set; }
      public int PercentageFunded => FundsNeeded>0 ? (int) Math.Round((decimal) ((100m/FundsNeeded) * FundsReceived), 0, MidpointRounding.AwayFromZero) : 0;
      public int IsDefault { get; set; }
    }
}
