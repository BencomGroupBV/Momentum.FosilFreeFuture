using System.Text.Json;
using Benchain.FosilFreeFuture.Service.Interfaces;
using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class ParticipantController :Controller
{
  private readonly ILogger<CustomerController> _logger;
  private readonly MomentumContext _context;

  public ParticipantController(ILogger<CustomerController> logger, MomentumContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<IActionResult> Index(int? profileId)
  {
    profileId ??= 3;

    var model = new ParticipantViewModel()
    {
      ProfileCard = new ProfileCardModel { CardTitle = "Initiator profile", Profile = new ProfileModel() },
      FundedProjectCard = new ProjectsCardModel
        { CardTitle = "Approved projects", Projects = new List<ProjectModel>(), Type = "approved" },
      ActiveProjectCard = new ProjectsCardModel
        { CardTitle = "To approve projects", Projects = new List<ProjectModel>(), Type = "toapprove" }
     };
    
    var profile = _context.ProfileDb.SingleOrDefault(p => p.Id == profileId);
    var projects = await _context.ProjectDb.ToListAsync();
    var approvedprojects = await _context.ApprovedProjectsDb.ToListAsync();

    model.ProfileCard.Profile = DbHelper.ParsProfileDb(profile);
    model.ProfileCard.Profile.Portfolio = new List<PortfolioModel>();
    model.ProfileCard.Profile.Badges = new List<BadgeModel>();
    model.FundedProjectCard.ProfileId = profileId.ToString();
    model.ActiveProjectCard.ProfileId = profileId.ToString();

    foreach (var projectdb in projects)
    {
       var approved = approvedprojects.FirstOrDefault(a => a.ParticipantId == profile.Id && a.ProjectId == projectdb.Id);
       if (approved != null) model.FundedProjectCard.Projects.Add(DbHelper.ParseProjectDb(projectdb));
    }

    foreach (var projectdb in projects.Where(p => p.Status == "open"))
    {
      var approved = approvedprojects.FirstOrDefault(a => a.ParticipantId == profile.Id && a.ProjectId == projectdb.Id);
      if (approved == null) model.ActiveProjectCard.Projects.Add(DbHelper.ParseProjectDb(projectdb));
    }

    string json;

    //File from URL (in wwwroot) OR from API

    //ServicePointManager.Expect100Continue = true;
    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    //json = (new WebClient()).DownloadString("https://localhost:7294/profile1.json");
    //model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel?>(json);


    //File from local files (in root project)
    //using (var r = new StreamReader("profile-klm.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.ProfileCard = JsonSerializer.Deserialize<ProfileCardModel>(json);
    //}

    //using (var r = new StreamReader("approvedprojects.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.FundedProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    //}

    //using (var r = new StreamReader("toapproveprojects.json"))
    //{
    //  json = r.ReadToEnd();
    //  model.ActiveProjectCard = JsonSerializer.Deserialize<ProjectsCardModel>(json);
    //}

    return View(model);
  }


  public IActionResult ApproveProject(int projectId, string profile)
  {
    var profileId = 0;

    if (profile != null)
    {
       profileId = int.Parse(profile);
    }

    var approved = _context.ApprovedProjectsDb.SingleOrDefault(a => a.ParticipantId == profileId && a.ProjectId == projectId);
    if (approved == null)
    {
      approved = new ApprovedProjectsDb
      {
        ParticipantId = profileId,
        ProjectId = projectId
      };
      _context.ApprovedProjectsDb.Add(approved);
      _context.SaveChanges();
    }

    return RedirectToAction("Index", new { profileId });

  }
}