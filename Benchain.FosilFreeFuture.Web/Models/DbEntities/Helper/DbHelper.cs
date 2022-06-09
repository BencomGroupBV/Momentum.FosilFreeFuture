using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;

namespace Benchain.FosilFreeFuture.Web;

public static class DbHelper
{
  public static ProjectModel ParseProjectDb(ProjectDb? projectDb)
  {
    if (projectDb == null) return null;
    var model = new ProjectModel
    {
      Id = $"{projectDb.Id}",
      Name = projectDb.Name,
      Country = projectDb.Country,
      Description = projectDb.Description,
      Logo = projectDb.Logo,
      Image = projectDb.Image,
      InitiatorWalletAddress = projectDb.Initiated,
      FundsNeeded = projectDb.FundsNeeded,
      FundsReceived = projectDb.FundsReceived,
      Status = projectDb.Status,
      ParticipantId = projectDb.ParticipantId
    };
    return model;
  }

  public static ProfileModel ParsProfileDb(ProfileDb? profileDb)
  {
    if (profileDb == null) return null;
    var model = new ProfileModel
    {
      Id = profileDb.Id,
      Name = profileDb.Name,
      Type = profileDb.Type,
      City = profileDb.City,
      Country = profileDb.Country,
      CurrentBalance = profileDb.CurrentBalance,
      Avatar = profileDb.Avatar
    };
    return model;
  }

  public static PortfolioModel ParsPortfolioDb(PortfolioDb? portfolioDb)
  {
    if(portfolioDb == null) return null;
    var model = new PortfolioModel()
    {
      Id = portfolioDb.Id,
      Name = portfolioDb.Name,
      Balance = portfolioDb.Balance,
      ParticipantId = portfolioDb.ParticipantId,
      ProfileId = portfolioDb.ProfileId
    };
    return model;
  }

  public static BadgeModel ParsBadgeDb(BadgeDb? badgeDb)
  {
    if(badgeDb==null) return null;
    var model = new BadgeModel()
    {
      Id = badgeDb.Id,
      Name = badgeDb.Name,
      Image = badgeDb.Image,
      Subtitle = badgeDb.Subtitle
    };
    return model;
  }
}

