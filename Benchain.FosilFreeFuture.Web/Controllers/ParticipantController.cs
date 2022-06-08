using System.Text.Json;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class ParticipantController :Controller
{
  public IActionResult Index()
  {
    var model = new ParticipantViewModel()
    {
      ProfileCard = new ProfileCardModel(),
    };

    string json;

    //File from URL (in wwwroot) OR from API

    //ServicePointManager.Expect100Continue = true;
    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    //json = (new WebClient()).DownloadString("https://localhost:7294/profile1.json");
    //model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel?>(json);


    //File from local files (in root project)
    using (var r = new StreamReader("profile-klm.json"))
    {
      json = r.ReadToEnd();
      model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    }

    using (var r = new StreamReader("approvedprojects.json"))
    {
      json = r.ReadToEnd();
      model.FundedProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    }

    using (var r = new StreamReader("toapproveprojects.json"))
    {
      json = r.ReadToEnd();
      model.ActiveProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    }

    return View(model);
  }
}