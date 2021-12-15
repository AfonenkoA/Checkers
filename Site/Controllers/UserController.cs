using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;

namespace Site.Controllers;

public sealed class UserController : Controller
{

    private static readonly IAsyncUserApi UserApi = new UserWebApi();
    private static readonly IAsyncResourceService ResourceApi = new AsyncResourceWebApi();

    public async Task<IActionResult> Profile([FromQuery] Credential c, [FromRoute] int id)
    {
        if (!c.IsValid) return View("Error");
        var (success, user) = await UserApi.TryGetUser(id);
        if (!success) return View("Error");
        var picture = ResourceApi.GetFileUrl(user.Picture.Resource.Id);
        var model = new PublicUserModel(c, user) { PictureUrl = picture };
        return View(model);
    }

    public async Task<IActionResult> List()
    {
        var (_, users) = await UserApi.TryGetUsersByNick("");
        return View(users);
    }

    public async Task<IActionResult> PersonalArea([FromQuery] Credential c)
    {
        var (success, self) = await UserApi.TryGetSelf(c);
        return !success ? View("Error") : View(new PublicUserModel(c,self));
    }
}
