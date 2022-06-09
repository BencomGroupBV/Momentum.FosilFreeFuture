using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Benchain.FosilFreeFuture.Service.Models;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class InitiatorController : Controller
{
  private readonly IProjectSmartContractService _projectSmartContract;

  public InitiatorController(IProjectSmartContractService projectSmartContract)
  {
    _projectSmartContract = projectSmartContract;
  }

  public IActionResult Index()
  {
    var model = new InitiatorViewModel()
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
    using (var r = new StreamReader("profile-co2ol.json"))
    {
      json = r.ReadToEnd();
      model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    }

    using (var r = new StreamReader("projects-co2ol.json"))
    {
      json = r.ReadToEnd();
      model.FundedProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    }

    model.Project = new ProjectModel
    {
      FundsNeeded = 0,
      FundsReceived = 0,
      Id = "1"
    };
    return View(model);
  }

  public IActionResult CreateNewProject(ProjectModel projectModel)
  {
    _projectSmartContract.CreateProject(projectModel);

    return RedirectToAction("Index");

  }


}