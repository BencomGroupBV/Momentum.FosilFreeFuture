using Benchain.FosilFreeFuture.Web.Models;

namespace Benchain.FosilFreeFuture.Web.ViewModels;

public class ParticipantViewModel
{
  public ProfileCardModel? ProfileCard { get; set; }
  public ProjectsCardModel? ApprovedProjectCard { get; set; }
  public ProjectsCardModel? OpenProjectCard { get; set; }
}