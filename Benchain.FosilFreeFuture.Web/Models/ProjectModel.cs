namespace Benchain.FosilFreeFuture.Web.Models
{
    public class ProjectModel
    {
      public string Name { get; set; }
      public string Country { get; set; }
      public string Description { get; set; }
      public string Logo { get; set; }
      public string Image { get; set; }
      public string Initiated { get; set; }
      public int FundsNeeded { get; set; }
      public int FundsReceived { get; set; }
      public string Status { get; set; }
      public int PercentageFunded => (int) Math.Round((decimal) ((100m/FundsNeeded) * FundsReceived), 0, MidpointRounding.AwayFromZero);
    }
}
