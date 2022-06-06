using System.Net;
using System.Text.Json;
using Benchain.FosilFreeFuture.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Benchain.FosilFreeFuture.Web.ViewModels;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class CustomerController : Controller
{
  private readonly ILogger<CustomerController> _logger;

  public CustomerController(ILogger<CustomerController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    var model = new CustomerViewModel
    {
      ProfileCard = new ProfileCardModel(),
      FundedProjectCard = new ProjectsCardModel(),
      ActiveProjectCard = new ProjectsCardModel()
    };

    string json;

    //File from URL (in wwwroot) OR from API

    //ServicePointManager.Expect100Continue = true;
    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    //json = (new WebClient()).DownloadString("https://localhost:7294/profile1.json");
    //model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel?>(json);


    //File from local files (in root project)
    using (var r = new StreamReader("profile1.json"))
    {
      json = r.ReadToEnd();
      model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    }

    using (var r = new StreamReader("fundedprojects.json"))
    {
      json = r.ReadToEnd();
      model.FundedProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    }

    using (var r = new StreamReader("activeprojects.json"))
    {
      json = r.ReadToEnd();
      model.ActiveProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    }


    return View(model);
  }

   
}