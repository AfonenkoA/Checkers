using System;
using Checkers.Api;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("api/"+WebApiBase.UserRoute)]
[ApiController]
public class UserController : Controller
{
    private static readonly IUserRepository Repository = new UserRepository();

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreationData user)
    {
        return Repository.CreateUser(user) ? OkResult : BadRequestResult;
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromQuery] Credential credential)
    {
        return Repository.DeleteUser(credential) ? OkResult : BadRequestResult;
    }

    [HttpGet("{id:int}")]
    public JsonResult GetUser([FromQuery] Credential credential, [FromRoute] int id)
    {
        return new JsonResult(Repository.GetUser(id));
    }

    //User Item Activities
    [HttpGet]
    public IActionResult ActionHandler([FromQuery] Credential credential, [FromQuery] string action, [FromQuery] string val)
    {
        return action switch
        {
            UserApiAction.SelectCheckersValue => SelectCheckers(credential, int.Parse(val)),
            UserApiAction.SelectAnimationValue => SelectAnimation(credential, int.Parse(val)),
            UserApiAction.AuthenticateValue => Authenticate(credential),
            UserApiAction.BuyValue => BuyItem(credential, int.Parse(val)),
            UserApiAction.UpdateNickValue => UpdateUserNick(credential, val),
            UserApiAction.UpdateLoginValue => UpdateUserLogin(credential, val),
            UserApiAction.UpdatePasswordValue => UpdateUserPassword(credential, val),
            UserApiAction.UpdateEmailValue => UpdateUserEmail(credential, val),
            UserApiAction.AddFriendValue => AddFriend(credential, int.Parse(val)),
            UserApiAction.DeleteFriendValue => DeleteFriend(credential, int.Parse(val)),
            UserApiAction.AcceptFriendValue => AcceptFriend(credential, int.Parse(val)),
            UserApiAction.GetUsersByNickValue => GetUsersByNick(val),
            _ => throw new NotImplementedException()
        };

    }

    private static IActionResult SelectAnimation(Credential credential, int animationId)
    {
        return Repository.SelectAnimation(credential,animationId) ? OkResult : BadRequestResult;
    }

    private static IActionResult SelectCheckers(Credential credential, int checkersId)
    {
        return Repository.SelectCheckers(credential,checkersId) ? OkResult : BadRequestResult;
    }

    private static IActionResult BuyItem(Credential credential, int itemId)
    {
        return new OkResult();
    }

    //User Account Activities 
    private static IActionResult Authenticate(Credential user)
    {
        return Repository.Authenticate(user) ? OkResult : BadRequestResult;
    }

    private static IActionResult UpdateUserNick(Credential credential, string nick)
    {
        return Repository.UpdateUserNick(credential,nick) ? OkResult : BadRequestResult;
    }

    private static IActionResult UpdateUserLogin(Credential credential, string login)
    {
        return Repository.UpdateUserLogin(credential,login) ? OkResult : BadRequestResult;
    }

    private static IActionResult UpdateUserPassword(Credential credential, string password)
    {
        return Repository.UpdateUserPassword(credential,password) ? OkResult : BadRequestResult;
    }

    private static IActionResult UpdateUserEmail(Credential credential, string email)
    {
        return Repository.UpdateUserEmail(credential,email) ? OkResult : BadRequestResult;
    }

    //Friends
    private static IActionResult GetUsersByNick(string pattern)
    {
        return new JsonResult(Repository.GetUsersByNick($"%{pattern}%"));
    }

    private static IActionResult AddFriend(Credential credential, int userId)
    {
        return new OkResult();
    }

    private static IActionResult DeleteFriend(Credential credential, int userId)
    {
        return new OkResult();
    }

    private static IActionResult AcceptFriend(Credential credential, int userId)
    {
        return new OkResult();
    }
}