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

    var projectModels = new List<ProjectModel>();
    var projectsOnBlockchainNetwork = _projectSmartContract.GetProjects();

    foreach (var project in projectsOnBlockchainNetwork)
    {
      var projectModel = new ProjectModel
      {
        Name = project.ProjectName,
        InitiatorWalletAddress = project.ProjectInitiated,
        Description = project.ProjectDescription,
        Country = project.ProjectCountry,
        FundsNeeded = (int)project.ProjectFundsNeeded,
        Image = project.ProjectImage,
        Status = project.ProjectStatus,
      };

      projectModels.Add(projectModel);
    }


    var model = new InitiatorViewModel()
    {
      ProfileCard = new ProfileCardModel
      {
        CardTitle = "Initiator profile",
        Profile = new ProfileModel()
      },
      FundedProjectCard = new ProjectsCardModel
      {
        CardTitle = "Your funded projects",
        Projects = new List<ProjectModel>(),
        Type = "closedproject"
      },
      ActiveProjectCard = new ProjectsCardModel
      {
        CardTitle = "Your active projects",
        Projects = new List<ProjectModel>(),
        Type = "openproject"
      },
      Project = new ProjectModel 
      { 
        Id = "1", 
        FundsNeeded = 0, 
        FundsReceived = 0 
      }
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

    model.ActiveProjectCard.Projects.AddRange(projectModels);

    if (projectId != null)
    {
      var editProjectdb = _context.ProjectDb.SingleOrDefault(p => p.Id == projectId);
      model.Project = DbHelper.ParseProjectDb(editProjectdb);
    }

    return View(model);
  }

  public IActionResult CreateNewProject(ProjectModel projectModel)
  {
    _projectSmartContract.CreateProject(projectModel);

    return RedirectToAction("Index");
  }
}

