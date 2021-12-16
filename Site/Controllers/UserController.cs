using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.User;
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

        var model = new Identified<IDictionary<long, UserInfo>>(credential,data);
        return View(model);
    }

    public async Task<ViewResult> UpdateNick(Credential c, string nick)
    {
        var success = await _repository.UpdateUserNick(c, nick);
        var model = new Identified<ModificationResult>(c,new ModificationResult("User", "PersonalArea", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdateLogin(Credential c, string newLogin)
    {
        var success = await _repository.UpdateUserLogin(c, newLogin);
        var model = new Identified<ModificationResult>(Credential.Invalid, new ModificationResult("Home", "Index", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdatePassword(Credential c, string newPassword)
    {
        var success = await _repository.UpdateUserPassword(c, newPassword);
        var model = new Identified<ModificationResult>(Credential.Invalid, new ModificationResult("Home", "Index", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdateEmail(Credential c, string email)
    {
        var success = await _repository.UpdateUserEmail(c, email);
        var model = new Identified<ModificationResult>(c, new ModificationResult("User", "PersonalArea", success));
        return View("ModificationResult", model);
    }
    
}
