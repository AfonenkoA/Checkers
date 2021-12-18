using Microsoft.AspNetCore.Mvc;
using Site.Data.Models.UserIdentity;


namespace Site.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index(Identity i) => View(i);
    public IActionResult AboutGame(Identity i) => View(i);
}