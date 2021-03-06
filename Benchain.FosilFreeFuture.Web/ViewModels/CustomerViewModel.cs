using Benchain.FosilFreeFuture.Web.Models;

namespace Benchain.FosilFreeFuture.Web.ViewModels
{
    public class CustomerViewModel
    {
      public ProfileCardModel? ProfileCard { get; set; }
      public ProjectsCardModel? FundedProjectCard { get; set; }
      public ProjectsCardModel? ActiveProjectCard { get; set; }
  }
}
