using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;


namespace Site.Controllers;

public sealed class AuthorizationController : Controller
{
    private static readonly IAsyncUserApi UserApi = new UserWebApi();
    public IActionResult Login() => View();

    public async Task<IActionResult> Authorize(Credential c)
    {
        var auth = await UserApi.Authenticate(c);
        return auth ?
            RedirectToAction("Index","Home",new {login = c.Login, password = c.Password }) :
            View("Error");
    }
}