using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;
using Benchain.FosilFreeFuture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class PortfolioController : Controller
{
  private readonly ILogger<CustomerController> _logger;
  private readonly MomentumContext _context;

  public PortfolioController(ILogger<CustomerController> logger, MomentumContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<IActionResult> Index()
  {
    var model = new CustomerViewModel
    {
      ProfileCard = new ProfileCardModel {CardTitle = "My profile", Profile = new ProfileModel()},
      FundedProjectCard = new ProjectsCardModel
        {CardTitle = "Funded projects", Projects = new List<ProjectModel>(), Type = "funded"},
      ActiveProjectCard = new ProjectsCardModel
        {CardTitle = "Active projects", Projects = new List<ProjectModel>(), Type = "active"}
    };
    var profileId = 1;
    var profile = _context.ProfileDb.SingleOrDefault(p => p.Id == profileId);
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
   

    foreach (var portfoliodb in portfolios.Where(p => p.ProfileId == profileId && p.Status == 1))
    {
      model.ProfileCard.Profile.Portfolio.Add(DbHelper.ParsPortfolioDb(portfoliodb));
    }

    foreach (var batchDb in batches.Where(p => p.ProfileId == profileId))
    {
      model.ProfileCard.Profile.Badges.Add(DbHelper.ParsBadgeDb(batchDb));
    }


    if (model.ProfileCard.Profile.Portfolio.Any())
    {
      model.ProfileCard.Profile.CurrentBalance = model.ProfileCard.Profile.Portfolio.Aggregate(0, (current, port) => current + port.Balance);
    }

    return View(model);
  }


  public IActionResult AddPortfolio(string code)
  {

    var portfolio = _context.PortfolioDb.SingleOrDefault(p => p.Id == 3);
    if (portfolio == null) return RedirectToAction("Index");

    portfolio.Status = 1; //Enable this portfolio Den haag
    _context.Update(portfolio);
    _context.SaveChanges();

    return RedirectToAction("Index", "Customer");

  }
  
}