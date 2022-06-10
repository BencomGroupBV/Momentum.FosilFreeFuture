using System.Net;
using System.Text.Json;
using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class CustomerController : Controller
{
  private readonly ILogger<CustomerController> _logger;
  private readonly MomentumContext _context;

  public CustomerController(ILogger<CustomerController> logger, MomentumContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<IActionResult> Index()
  {
    var model = new CustomerViewModel
    {
      ProfileCard = new ProfileCardModel{ CardTitle = "My profile", Profile = new ProfileModel()},
      FundedProjectCard = new ProjectsCardModel{CardTitle = "Funded projects", Projects = new List<ProjectModel>(), Type = "funded"},
      ActiveProjectCard = new ProjectsCardModel { CardTitle = "Active projects", Projects = new List<ProjectModel>(), Type = "active" }
    };
    var profileId = 1;
    var profile =  _context.ProfileDb.SingleOrDefault(p => p.Id == profileId);
    var profilesDb = await _context.ProfileDb.ToListAsync();
    var portfolios = await _context.PortfolioDb.ToListAsync();
    var batches = await _context.BadgeDb.ToListAsync();
    var projects = await _context.ProjectDb.ToListAsync();
    var approvedprojects = await _context.ApprovedProjectsDb.ToListAsync();
    var fundedprojects = await _context.FundedProjectDb.ToListAsync();

    model.ProfileCard.Profile = DbHelper.ParsProfileDb(profile);
    model.ProfileCard.Profile.Portfolio = new List<PortfolioModel>();
    model.ProfileCard.Profile.Badges = new List<BadgeModel>();

    model.FundedProjectCard.ProfileId = profileId.ToString();
    model.ActiveProjectCard.ProfileId = profileId.ToString();

    foreach (var portfoliodb in portfolios.Where(p=>p.ProfileId==profileId))
    {
      model.ProfileCard.Profile.Portfolio.Add(DbHelper.ParsPortfolioDb(portfoliodb));

      foreach (var projectdb in projects)
      {
        var approved = approvedprojects.FirstOrDefault(a => a.ParticipantId == portfoliodb.ParticipantId && a.ProjectId == projectdb.Id);
        if (approved != null)
        {
          var funded = fundedprojects.FirstOrDefault(f =>
            f.ParticipantId == approved.ParticipantId && f.ProjectId == projectdb.Id && f.ProfileId == profile.Id);
          if (funded != null)
          {
            var projectmodel = DbHelper.ParseProjectDb(projectdb);
            projectmodel.Logo = profilesDb.FirstOrDefault(p => p.Id == approved.ParticipantId).Avatar.Remove(0, 5);
            ;
            projectmodel.ParticipantId = approved.ParticipantId;
            model.FundedProjectCard.Projects.Add(projectmodel);
          }
          else
          {
            var projectmodel = DbHelper.ParseProjectDb(projectdb);
            projectmodel.Logo = profilesDb.FirstOrDefault(p => p.Id == approved.ParticipantId).Avatar.Remove(0, 5);
            projectmodel.ParticipantId = approved.ParticipantId;
            model.ActiveProjectCard.Projects.Add(projectmodel);
          }
        }
      }
    }

    foreach (var batchDb in batches.Where(p => p.ProfileId == profileId))
    {
      model.ProfileCard.Profile.Badges.Add(DbHelper.ParsBadgeDb(batchDb));
    }


    string json;

    //File from URL (in wwwroot) OR from API

    //ServicePointManager.Expect100Continue = true;
    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    //json = (new WebClient()).DownloadString("https://localhost:7294/profile1.json");
    //model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel?>(json);
    
    //File from local files (in root project)
    //using (var r = new StreamReader("profile1.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    //}

    //using (var r = new StreamReader("fundedprojects.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.FundedProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    //}

    //using (var r = new StreamReader("activeprojects.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.ActiveProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    //}

    if (model.ProfileCard.Profile.Portfolio.Any())
    {
     model.ProfileCard.Profile.CurrentBalance = model.ProfileCard.Profile.Portfolio.Aggregate(0, (current, port) => current + port.Balance);
    }

    return View(model);
  }

  public IActionResult FundProject(int projectId, int profileId, string participantId)
  {

    var participant = 0;

    if (participantId != null)
    {
      participant = int.Parse(participantId);
    }

    var funded = _context.FundedProjectDb.SingleOrDefault(a => a.ParticipantId == participant  && a.ProjectId == projectId && a.ProfileId == profileId);
    if (funded == null)
    {
      funded = new FundedProjectDb
      {
        ParticipantId = participant,
        ProjectId = projectId,
        ProfileId = profileId

      };
      _context.FundedProjectDb.Add(funded);
      _context.SaveChanges();
    }

    return RedirectToAction("Index", new { profileId });

  }

}