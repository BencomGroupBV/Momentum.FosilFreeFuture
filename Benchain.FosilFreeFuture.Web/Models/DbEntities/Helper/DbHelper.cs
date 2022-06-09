using Benchain.FosilFreeFuture.Service.Models;
using Benchain.FosilFreeFuture.Web.Models.DbEntities;

namespace Benchain.FosilFreeFuture.Web
{
  public static class DbHelper
  {
    public static ProjectModel ParseProjectDb(ProjectDb projectDb)
    {
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
      };
      return model;
    }
  }
}
