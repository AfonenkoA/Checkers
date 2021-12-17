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

    protected async Task<(bool,IIdentity)> Authorize(string login, string password)
    {
        var (success, self) = await _userRepository.GetSelf(new Credential
        {
            Login = login,
            Password = password
        });
        return (success, new Identity(login, password, self.Type));
    }

    protected static object IdentityValues(IIdentity identity) => new { login = identity.Login, password = identity.Password, type = identity.Type };
}