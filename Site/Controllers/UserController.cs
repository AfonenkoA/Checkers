using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.User;
using Site.Data.Models.UserIdentity;
using Site.Service.Interface;

namespace Site.Controllers;

public sealed class UserController : ControllerBase
{

    private readonly IUserService _repository;

    public UserController(IUserService repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Profile(Identity i, int id)
    {
        var (success, user) = await _repository.GetUser(id);
        var model = new Identified<UserInfo>(i, user);
        return !success ? View("Error") : View(model);
    }


    public async Task<IActionResult> PersonalArea(Credential c)
    {
        if (!c.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "PersonalArea", callerController = "User" });
        var (s,i) = await Authorize(c.Login, c.Password);
        var (success, self) = await _repository.GetSelf(c);
        var model = new Identified<Self>(i, self);
        return success ? View(model) : View("Error");
    }

    public async Task<ViewResult> TopPlayers(Identity i)
    {
        IDictionary<long, UserInfo> data;
        bool success;
        if (i.IsValid)
        {
            (success, data) = await _repository.GetTop(i);
            if (!success) return View("Error");
        }
        else
        {
            (success, data) = await _repository.GetTop();
            if (!success) return View("Error");
        }
        var model = new Identified<IDictionary<long, UserInfo>>(i, data);
        return View(model);
    }

    public async Task<ViewResult> UpdateNick(Identity i, string nick)
    {
        var success = await _repository.UpdateUserNick(i, nick);
        var model = new Identified<ModificationResult>(i, new ModificationResult("User", "PersonalArea", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdateLogin(Identity i, string newLogin)
    {
        var success = await _repository.UpdateUserLogin(i, newLogin);
        var model = new Identified<ModificationResult>(Identity.Invalid, new ModificationResult("Home", "Index", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdatePassword(Identity i, string newPassword)
    {
        var success = await _repository.UpdateUserPassword(i, newPassword);
        var model = new Identified<ModificationResult>(Identity.Invalid, new ModificationResult("Home", "Index", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdateEmail(Identity i, string email)
    {
        var success = await _repository.UpdateUserEmail(i, email);
        var model = new Identified<ModificationResult>(i, new ModificationResult("User", "PersonalArea", success));
        return View("ModificationResult", model);
    }

    public async Task<ViewResult> UpdatePicture(Identity i, int picture)
    {
        var success = await _repository.UpdateUserPicture(i, picture);
        var model = new Identified<ModificationResult>(i, new ModificationResult("User", "PersonalArea", success));
        return View("ModificationResult", model);
    }



    public IActionResult CreationPage(Identity i) => View(i);


    public async Task<IActionResult> Create(UserCreationData d)
    {
        var success = await _repository.CreateUser(d);
        return success ? RedirectToAction("Login", "Authorization") : View("Error");
    }

}
