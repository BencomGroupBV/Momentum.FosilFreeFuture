using Benchain.FosilFreeFuture.Service.Models;

namespace Benchain.FosilFreeFuture.Web.Models
{
  public class ProjectsCardModel
  {
    public string CardTitle { get; set; }
    public string Type { get; set; }
    public string ProfileId { get; set; }
    public List<ProjectModel>? Projects { get; set; }
  }
}
