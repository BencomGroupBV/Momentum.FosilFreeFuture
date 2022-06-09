﻿using System.Net;
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
    var portfolios = await _context.PortfolioDb.ToListAsync();
    var batches = await _context.BadgeDb.ToListAsync();
    var projects = await _context.ProjectDb.ToListAsync();
    var approvedprojects = await _context.ApprovedProjectsDb.ToListAsync();
    var participants = await _context.ParticipantDb.ToListAsync();

    model.ProfileCard.Profile = DbHelper.ParsProfileDb(profile);
    model.ProfileCard.Profile.Portfolio = new List<PortfolioModel>();
    model.ProfileCard.Profile.Badges = new List<BadgeModel>();
    foreach (var portfoliodb in portfolios.Where(p=>p.ProfileId==profileId))
    {
      model.ProfileCard.Profile.Portfolio.Add(DbHelper.ParsPortfolioDb(portfoliodb));

      foreach (var projectdb in projects.Where(p=> p.Status == "closed"))
      {
        var approved = approvedprojects.FirstOrDefault(a => a.ParticipantId == portfoliodb.ParticipantId && a.ProjectId == projectdb.Id);
        if (approved != null)
        {
          var projectmodel = DbHelper.ParseProjectDb(projectdb);
          projectmodel.Logo = participants.FirstOrDefault(p => p.Id == approved.ParticipantId).Logo;
          model.FundedProjectCard.Projects.Add(projectmodel);
        }
      }
      foreach (var projectdb in projects.Where(p => p.Status == "open"))
      {
        var approved = approvedprojects.FirstOrDefault(a => a.ParticipantId == portfoliodb.ParticipantId && a.ProjectId == projectdb.Id);
        if (approved != null)
        {
          var projectmodel = DbHelper.ParseProjectDb(projectdb);
          projectmodel.Logo = participants.FirstOrDefault(p => p.Id == approved.ParticipantId).Logo;
          model.ActiveProjectCard.Projects.Add(projectmodel);
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

   
}