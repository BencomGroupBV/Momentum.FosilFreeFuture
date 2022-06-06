using Microsoft.AspNetCore.Mvc;
using Benchain.FosilFreeFuture.Service.Interfaces;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class InitiatorController : Controller
{
  private readonly IProjectContractService _web3;

  public InitiatorController(IProjectContractService web3)
  {
    _web3 = web3;
  }

  public IActionResult Index()
  {
    return View();
  }

  public void CreateProject()
  {
    _web3.CreateProject();
  }

}