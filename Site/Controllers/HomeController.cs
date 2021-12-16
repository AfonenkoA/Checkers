using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;


namespace Site.Controllers;

public sealed class HomeController : Controller
{

    public IActionResult Index(Credential c) => View(c);
}