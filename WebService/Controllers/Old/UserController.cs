using System;
using System.Linq;
using System.Text.Json;
using Checkers.Data.Old;
using Checkers.Game.Old;
using Microsoft.AspNetCore.Mvc;
using static Checkers.CommunicationProtocol;

namespace WebService.Controllers.Old;

[Route("api/user")]
[ApiController]
public class UserController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    public UserController(GameDatabase.Factory factory)
    {
        database = factory.Get();
    }

    private readonly GameDatabase database;

    [HttpGet("{login}")]
    public string SimpleUserGet([FromRoute] string login)
    {
        var user = database.FindUser(login);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        return Serialize(
            new UserGetResponse
            {
                Status = ResponseStatus.Ok,
                Info = new UserInfo
                {
                    Nick = user.Nick,
                    Raiting = user.Rating,
                    PictureId = user.PictureId,
                    LastActivity = user.LastActivity
                }
            });


    }

    [HttpGet]
    public string SequreUserGet([FromQuery] string login, [FromQuery] string password, [FromQuery] string action)
    {
        User user = database.FindUser(login, password);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        return action switch
        {
            "info" => Serialize(
                new UserInfoResponse
                {
                    Status = ResponseStatus.Ok,
                    Info = new UserInfo
                    {
                        Nick = login,
                        LastActivity = DateTime.Now,
                        PictureId = 1,
                        Raiting = 1000
                    },
                    Email = login + "@example.com",
                }),
            "authorize" => Serialize(new UserAuthorizationResponse { Status = ResponseStatus.Ok }),
            _ => Serialize(BasicResponse.Failed),
        };
    }

    [HttpPost]
    public string SequreUserPost([FromBody] JsonElement json)
    {
        UserUpdateRequest request = Deserialize<UserUpdateRequest>(json.ToString());
        return Serialize(
            new UserInfoResponse
            {
                Status = ResponseStatus.Ok,
                Email = request.Login + "@example.com",
                Info = new UserInfo
                {
                    Nick = request.Login,
                    LastActivity = DateTime.Now,
                    PictureId = 1,
                    Raiting = 1000
                }
            });
    }

    [HttpDelete]
    public string Delete([FromQuery] string login, [FromQuery] string password)
    {
        return Serialize(new UserDeleteResponse { Status = ResponseStatus.Ok });
    }

    [HttpGet("{login}/items")]
    public string SequreUserItemsGet([FromRoute] string login, [FromQuery] string password)
    {
        User user = database.FindUser(login, password);
        if (user == null)
            return Serialize(BasicResponse.Failed);

        var items = from item in database.Items
            where user.Id == item.UserId
            select item.ItemId;

        return Serialize(new UserItemsResponse
        {
            Status = ResponseStatus.Ok,
            SelectedAnimationsId = 1,
            SelectedCheckersId = 1,
            Items = items.ToArray()
        }); ;
    }

    [HttpPut("{login}/items")]
    public string SequreUserItemsPut([FromRoute] string login, [FromBody] JsonElement json)
    {
        UserItemsUpdateRequest request = Deserialize<UserItemsUpdateRequest>(json.ToString());
        return Serialize(new UserItemsResponse
        {
            Status = ResponseStatus.Ok,
            SelectedAnimationsId = 1,
            SelectedCheckersId = 1,
            Items = new int[] { 1, 2, 3 }
        });
    }

    [HttpGet("{login}/achievements")]
    public string UserAchievementsGet([FromRoute] string login)
    {
        User user = database.FindUser(login);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        var achievements = from a in database.Achievements
            where a.UserId == user.Id
            select a.Id;
        return Serialize(new UserAchievementsGetResponse
        {
            Status = ResponseStatus.Ok,
            Achievements = achievements.ToArray()
        });
    }

    [HttpGet("{login}/friends")]
    public string UserFriendsGet([FromRoute] string login, [FromQuery] string password)
    {
        User user = database.FindUser(login, password);
        if (user == null)
            return Serialize(BasicResponse.Failed);

        var result = from uf in database.Friends
            where uf.UserId == user.Id
            join u in database.Users on uf.FriendId equals u.Id
            select u.Login;

        return Serialize(new UserFriendsResponse
        {
            Status = ResponseStatus.Ok,
            Friends = result.ToArray()
        });
    }

    [HttpPut("{login}/friends")]
    public string UserFriendsPut([FromRoute] string login, [FromBody] JsonElement json)
    {
        UserFriendsUpdateRequest request = Deserialize<UserFriendsUpdateRequest>(json.ToString());
        return Serialize(new UserFriendsResponse
        {
            Status = ResponseStatus.Ok,
            Friends = new string[] { "biba", "boba" }
        }); ;
    }

    [HttpGet("{login}/games")]
    public string UserGamesGet([FromRoute] string login)
    {
        return Serialize(new UserGamesGetResponse
        {
            Status = ResponseStatus.Ok,
            Games = new int[] { 1, 2, 3 }
        });
    }
}