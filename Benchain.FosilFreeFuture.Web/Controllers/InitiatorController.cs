using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class InitiatorController : Controller
{
  private readonly IProjectSmartContractService _projectSmartContract;
  private readonly ILogger<CustomerController> _logger;
  private readonly MomentumContext _context;

  public InitiatorController(IProjectSmartContractService projectSmartContract, ILogger<CustomerController> logger, MomentumContext context)
  {
    _projectSmartContract = projectSmartContract;
    _logger = logger;
    _context = context;
  }

  public async Task<IActionResult> Index(int? profileId, int? projectId)
  {
    profileId ??= 2;

    var model = new InitiatorViewModel()
    {
      ProfileCard = new ProfileCardModel {CardTitle = "Initiator profile", Profile = new ProfileModel()},
      FundedProjectCard = new ProjectsCardModel
        {CardTitle = "Your funded projects", Projects = new List<ProjectModel>(), Type = "closedproject"},
      ActiveProjectCard = new ProjectsCardModel
        {CardTitle = "Your active projects", Projects = new List<ProjectModel>(), Type = "openproject"},
      Project = new ProjectModel {Id = "1", FundsNeeded = 0, FundsReceived = 0}
    };

    
    var profile = _context.ProfileDb.SingleOrDefault(p => p.Id == profileId);
    var projects = await _context.ProjectDb.ToListAsync();

    model.ProfileCard.Profile = DbHelper.ParsProfileDb(profile);
    model.ProfileCard.Profile.Portfolio = new List<PortfolioModel>();
    model.ProfileCard.Profile.Badges = new List<BadgeModel>();
    
    foreach (var projectdb in projects.Where(p => p.Initiated == "CO2OL" && p.Status == "closed"))
    {
      model.FundedProjectCard.Projects.Add(DbHelper.ParseProjectDb(projectdb));
    }

    foreach (var projectdb in projects.Where(p => p.Initiated == "CO2OL" && p.Status == "open"))
    {
      model.ActiveProjectCard.Projects.Add(DbHelper.ParseProjectDb(projectdb));
    }

    if (projectId != null)
    {
      var editProjectdb = _context.ProjectDb.SingleOrDefault(p => p.Id == projectId);
      model.Project = DbHelper.ParseProjectDb(editProjectdb);
    }


    string json;

    //File from URL (in wwwroot) OR from API

    //ServicePointManager.Expect100Continue = true;
    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    //json = (new WebClient()).DownloadString("https://localhost:7294/profile1.json");
    //model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel?>(json);


    //File from local files (in root project)
    //using (var r = new StreamReader("profile-co2ol.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    //}

    //using (var r = new StreamReader("projects-co2ol.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.FundedProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    //}

    return View(model);
  }

  public IActionResult CreateNewProject(ProjectModel projectModel)
  {
    _projectSmartContract.CreateProject(projectModel);

    var projectModels = new List<ProjectModel>();
    var projects = _projectSmartContract.GetProjects();

    foreach (var project in projects)
    {
      var model = new ProjectModel
      {
        Name = project.ProjectName,
        Description = project.ProjectDescription,

      };
      projectModels.Add(model);
    }

    var viewModel = new InitiatorViewModel
    {
      ActiveProjectCard = new ProjectsCardModel
      {
        Projects = projectModels
      }
    };

    return View(viewModel);
  }
}

