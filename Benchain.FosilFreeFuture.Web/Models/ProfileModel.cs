namespace Benchain.FosilFreeFuture.Web.Models
{
    public class ProfileModel
    {
      public int Id { get; set; }

      public string Type { get; set; }

      public string Name { get; set; }
      
      public string City { get; set; }
      
      public string Country { get; set; }
      
      public int CurrentBalance { get; set; }

      public string Avatar { get; set; }

      public List<PortfolioModel> Portfolio { get; set; }

      public List<BadgeModel> Badges { get; set; }
    }
}
