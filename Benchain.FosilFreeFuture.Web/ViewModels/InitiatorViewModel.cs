using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models;

namespace Benchain.FosilFreeFuture.Web.ViewModels
{
    public class InitiatorViewModel
  {
      public ProfileCardModel? ProfileCard { get; set; }
      public ProjectsCardModel? FundedProjectCard { get; set; }
      public ProjectsCardModel? ActiveProjectCard { get; set; }

      public ProjectModel Project { get; set; }
  }
}
