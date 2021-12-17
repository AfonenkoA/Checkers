using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models.UserIdentity;
using Site.Repository.Interface;

namespace Site.Controllers;

public class ControllerBase : Controller
{
    private readonly IUserRepository _userRepository;

    public ControllerBase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected async Task<(bool, IIdentity)> Authorize(string login, string password)
    {
        var c = new Credential { Login = login, Password = password };
        var (success, self) = await _userRepository.GetSelf(c);
        return (success, new Identity(c, self));
    }

    protected static object IdentityValues(IIdentity identity) => new
    {
        login = identity.Login,
        password = identity.Password,
        type = identity.Type,
        userId = identity.UserId
    };
}