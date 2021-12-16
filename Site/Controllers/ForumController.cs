using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers;

public sealed class ForumController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Post()
    {
        return View();
    }
}