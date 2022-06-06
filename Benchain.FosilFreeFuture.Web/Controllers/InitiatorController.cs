using Microsoft.AspNetCore.Mvc;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class InitiatorController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}