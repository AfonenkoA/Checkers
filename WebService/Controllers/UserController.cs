using System;
using Checkers.Api;
using Checkers.Api.Interface.Action;
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
    public UserController(Repository.Factory factory)
    {
        _repository = factory.Get<UserRepository>();
    }

    private readonly IUserRepository _repository;

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreationData user)
    {
        return _repository.CreateUser(user) ? OkResult : BadRequestResult;
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromQuery] Credential credential)
    {
        return _repository.DeleteUser(credential) ? OkResult : BadRequestResult;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetUser([FromQuery] Credential credential, [FromRoute] int id)
    {
        return Json(_repository.GetUser(id));
    }

    //User Item Activities
    [HttpPost]
    public IActionResult ActionHandler([FromQuery] Credential credential, [FromQuery] string action, [FromBody] string val)
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

    private IActionResult SelectAnimation(Credential credential, int animationId)
    {
        return _repository.SelectAnimation(credential,animationId) ? OkResult : BadRequestResult;
    }

    private IActionResult SelectCheckers(Credential credential, int checkersId)
    {
        return _repository.SelectCheckers(credential,checkersId) ? OkResult : BadRequestResult;
    }

    private static IActionResult BuyItem(Credential credential, int itemId)
    {
        return BadRequestResult;
    }

    //User Account Activities 
    private IActionResult Authenticate(Credential user)
    {
        return _repository.Authenticate(user) ? OkResult : BadRequestResult;
    }

    private IActionResult UpdateUserNick(Credential credential, string nick)
    {
        return _repository.UpdateUserNick(credential,nick) ? OkResult : BadRequestResult;
    }

    private IActionResult UpdateUserLogin(Credential credential, string login)
    {
        return _repository.UpdateUserLogin(credential,login) ? OkResult : BadRequestResult;
    }

    private IActionResult UpdateUserPassword(Credential credential, string password)
    {
        return _repository.UpdateUserPassword(credential,password) ? OkResult : BadRequestResult;
    }

    private IActionResult UpdateUserEmail(Credential credential, string email)
    {
        return _repository.UpdateUserEmail(credential,email) ? OkResult : BadRequestResult;
    }

    //Friends
    private IActionResult GetUsersByNick(string pattern)
    {
        return Json(_repository.GetUsersByNick($"%{pattern}%"));
    }

    private IActionResult AddFriend(Credential credential, int userId)
    {
        return _repository.AddFriend(credential,userId) ? OkResult : BadRequestResult;
    }

    private static IActionResult DeleteFriend(Credential credential, int userId)
    {
        return BadRequestResult;
    }

    private static IActionResult AcceptFriend(Credential credential, int userId)
    {
        return BadRequestResult;
    }
}