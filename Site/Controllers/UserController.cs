using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.User;
using Site.Repository;
using Site.Repository.Interface;

namespace Site.Controllers;

public sealed class UserController : Controller
{

    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Profile(string login, string password, int id)
    {
        var c = new Credential { Login = login, Password = password };
        var (success, user) = await _repository.GetUser(id);
        var model = new Identified<UserInfo>(c, user);
        return !success ? View("Error") : View(model);
    }


    public async Task<IActionResult> PersonalArea(Credential c)
    {
        if (!c.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "PersonalArea", callerController = "User" });
        var (success, self) = await _repository.GetSelf(c);
        var model = new Identified<Self>(c, self);
        return success ? View(model) : View("Error");
    }

    public async Task<ViewResult> TopPlayers(Credential credential)
    {
        IDictionary<long, UserInfo> data;
        bool success;
        if (credential.IsValid)
        {
            (success, data) = await _repository.GetTop(credential);
            if (!success) return View("Error");
        }
        else
        {
            (success, data) = await _repository.GetTop();
            if (!success) return View("Error");
        }

        var players = new Dictionary<long, IIdentified<UserInfo>>();
        foreach (var (pos,user) in data)
            players.Add(pos,new Identified<UserInfo>(credential,user));
        var model = new Identified<IDictionary<long, IIdentified<UserInfo>>>(credential, players);
        return View(model);
    }
}
