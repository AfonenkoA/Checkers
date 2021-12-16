using Common.Entity;
using Microsoft.AspNetCore.Mvc;


namespace Site.Controllers;

public sealed class HomeController : Controller
{

    public IActionResult Index(Credential c) => View(c);
}