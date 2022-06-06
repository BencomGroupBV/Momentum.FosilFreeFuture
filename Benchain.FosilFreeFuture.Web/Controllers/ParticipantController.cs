using System.Text.Json;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class ParticipantController :Controller
{
  public IActionResult Index()
  {
    var model = new ParticipantViewModel
    {
      ProfileCard = new ProfileCardModel(),
      ApprovedProjectCard =  new ProjectsCardModel(),
      OpenProjectCard = new ProjectsCardModel()
    };

    string json;
    
    using (var r = new StreamReader("profile-klm.json"))
    {
      json = r.ReadToEnd();
      model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    }

    return View(model);
  }
}