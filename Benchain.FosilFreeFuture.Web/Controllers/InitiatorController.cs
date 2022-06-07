using Microsoft.AspNetCore.Mvc;
using Benchain.FosilFreeFuture.Service.Interfaces;

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
    return View();
  }

  public void CreateProject()
  {
    _projectSmartContract.CreateProject();
  }

}