using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.UserIdentity;
using Site.Service.Interface;

namespace Site.Controllers;

public sealed class AuthorizationController : ControllerBase
{
    private readonly IUserService _repository;

    public AuthorizationController(IUserService repository) : base(repository)
    {
        _repository = repository;
    }

    public IActionResult Login(string callerController, string callerAction) =>
        View(new Caller(callerController, callerAction));

    public async Task<IActionResult> Authorize(Credential c, Caller caller)
    {
        var (success, self) = await _repository.GetSelf(c);
        if (!success) return View("Error");
        var model = new Identity(c, self);
        return RedirectToAction(caller.CallerAction, caller.CallerController, IdentityValues(model));
    }
}