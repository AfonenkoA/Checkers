using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;
using static Checkers.Api.Interface.Action.UserApiAction;
using static Checkers.Api.WebImplementation.WebApiBase;

namespace WebService.Controllers;

[Route("api/" + UserRoute)]
[ApiController]
public class UserController : ControllerBase
{
    public UserController(RepositoryFactory factory) => _repository = factory.Get<UserRepository>();

    private readonly IUserRepository _repository;

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreationData user) =>
        _repository.CreateUser(user) ? OkResult : BadRequestResult;

    [HttpDelete]
    public IActionResult DeleteUser([FromQuery] Credential credential) =>
        _repository.DeleteUser(credential) ? OkResult : BadRequestResult;

    //доработать friend
    [HttpGet("{id:int}")]
    public IActionResult GetUser([FromQuery] Credential credential, [FromRoute] int id) =>
        Json(credential.IsValid ? _repository.GetFriend(credential,id) : _repository.GetUser(id));

    [HttpGet]
    public IActionResult GetSelf([FromQuery] Credential credential) =>
        Json(_repository.GetSelf(credential));


    [HttpGet, Route("shop/animation")]
    public IActionResult GetAvailableAnimation([FromQuery] Credential credential) =>
        Json(_repository.GetAvailableAnimations(credential));

    [HttpGet, Route("shop/checkers-skin")]
    public IActionResult GetAvailableCheckersSkin([FromQuery] Credential credential) =>
        Json(_repository.GetAvailableCheckers(credential));

    [HttpGet, Route("shop/lootbox")]
    public IActionResult GetAvailableLootBox([FromQuery] Credential credential) =>
        Json(_repository.GetAvailableLootBoxes(credential));

    [HttpPut]
    public IActionResult ActionHandler([FromQuery] Credential credential, [FromQuery] string action, [FromBody] string val) =>
        action switch
        {
            SelectCheckersValue => SelectCheckers(credential, int.Parse(val)),
            SelectAnimationValue => SelectAnimation(credential, int.Parse(val)),
            AuthenticateValue => Authenticate(credential),
            BuyValue => BuyItem(credential, int.Parse(val)),
            UpdateNickValue => UpdateUserNick(credential, val),
            UpdateLoginValue => UpdateUserLogin(credential, val),
            UpdatePasswordValue => UpdateUserPassword(credential, val),
            UpdateEmailValue => UpdateUserEmail(credential, val),
            AddFriendValue => AddFriend(credential, int.Parse(val)),
            DeleteFriendValue => DeleteFriend(credential, int.Parse(val)),
            AcceptFriendValue => AcceptFriend(credential, int.Parse(val)),
            GetUsersByNickValue => GetUsersByNick(val),
            UpdateUserPictureValue => UpdateUserPicture(credential,int.Parse(val)),
            BuyAnimationValue => BuyAnimation(credential,int.Parse(val)),
            BuyCheckersSkinValue => BuyCheckersSkin(credential,int.Parse(val)),
            BuyLootBoxValue => BuyLootBox(credential,int.Parse(val)),
            _ => BadRequestResult
        };

    private IActionResult UpdateUserPicture(Credential credential, int pictureId)
        => _repository.UpdateUserPicture(credential, pictureId) ? OkResult : BadRequestResult;

    private IActionResult SelectAnimation(Credential credential, int animationId) =>
        _repository.SelectAnimation(credential, animationId) ? OkResult : BadRequestResult;

    private IActionResult SelectCheckers(Credential credential, int checkersId) =>
        _repository.SelectCheckers(credential, checkersId) ? OkResult : BadRequestResult;

    private static IActionResult BuyItem(Credential credential, int itemId)
        => BadRequestResult;

    //User Account Activities 
    private IActionResult Authenticate(Credential user) =>
        _repository.Authenticate(user) ? OkResult : BadRequestResult;

    private IActionResult UpdateUserNick(Credential credential, string nick) =>
        _repository.UpdateUserNick(credential, nick) ? OkResult : BadRequestResult;

    private IActionResult UpdateUserLogin(Credential credential, string login) =>
        _repository.UpdateUserLogin(credential, login) ? OkResult : BadRequestResult;

    private IActionResult UpdateUserPassword(Credential credential, string password) =>
        _repository.UpdateUserPassword(credential, password) ? OkResult : BadRequestResult;

    private IActionResult UpdateUserEmail(Credential credential, string email) =>
        _repository.UpdateUserEmail(credential, email) ? OkResult : BadRequestResult;

    //Friends
    private IActionResult GetUsersByNick(string pattern) =>
        Json(_repository.GetUsersByNick($"%{pattern}%"));

    private IActionResult AddFriend(Credential credential, int userId) =>
        _repository.AddFriend(credential, userId) ? OkResult : BadRequestResult;

    private static IActionResult DeleteFriend(Credential credential, int userId) => BadRequestResult;

    private static IActionResult AcceptFriend(Credential credential, int userId) => BadRequestResult;

    private IActionResult BuyAnimation(Credential credential, int id) =>
        _repository.BuyAnimation(credential, id) ? OkResult : BadRequestResult;

    private IActionResult BuyCheckersSkin(Credential credential, int id) =>
        _repository.BuyCheckersSkin(credential, id) ? OkResult : BadRequestResult;

    private IActionResult BuyLootBox(Credential credential, int id) =>
        _repository.BuyLootBox(credential, id) ? OkResult : BadRequestResult;
}