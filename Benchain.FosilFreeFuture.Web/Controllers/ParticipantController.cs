using Microsoft.AspNetCore.Mvc;

namespace Benchain.FosilFreeFuture.Web.Controllers;

public class ParticipantController :Controller
{
  public IActionResult Index()
  {
    return View();
  }
}