using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;

namespace Site.Controllers;

public sealed class AuthorizationController : Controller
{
    private static readonly IAsyncUserApi UserApi = new UserWebApi();


    public IActionResult Login(string callerController, string callerAction) =>
        View(new Caller(callerController, callerAction));

    public async Task<IActionResult> Authorize(string login, string password, string callerController, string callerAction)
    {
        var auth = await UserApi.Authenticate(new Credential { Login = login, Password = password });
        return auth ?
            RedirectToAction(callerAction, callerController, new { login = login, password = password }) :
            View("Error");
    }
}