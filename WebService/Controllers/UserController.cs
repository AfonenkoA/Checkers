using System;
using System.Linq;
using System.Text.Json;
using Checkers.Data;
using Checkers.Game;
using Microsoft.AspNetCore.Mvc;
using static Checkers.CommunicationProtocol;
using static Checkers.Game.ResponseStatus;

namespace WebService.Controllers;

[Route("api/user")]
[ApiController]
public class UserController
{
    public UserController(GameDatabase.Factory factory)
    {
        _database = factory.Get();
    }

    private readonly GameDatabase _database;

    [HttpGet("{login}")]
    public string SimpleUserGet([FromRoute] string login)
    {
        var user = _database.FindUser(login);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        return Serialize(
            new UserGetResponse
            {
                Status = Ok,
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
        var user = _database.FindUser(login, password);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        return action switch
        {
            "info" => Serialize(
                new UserInfoResponse
                {
                    Status = Ok,
                    Info = new UserInfo
                    {
                        Nick = login,
                        LastActivity = DateTime.Now,
                        PictureId = 1,
                        Raiting = 1000
                    },
                    Email = login + "@example.com",
                }),
            "authorize" => Serialize(new UserAuthorizationResponse { Status = Ok }),
            _ => Serialize(BasicResponse.Failed),
        };
    }

    [HttpPost]
    public string SequreUserPost([FromBody] JsonElement json)
    {
        var request = Deserialize<UserUpdateRequest>(json.ToString());
        return Serialize(
            new UserInfoResponse
            {
                Status = Ok,
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
        return Serialize(new UserDeleteResponse { Status = Ok });
    }

    [HttpGet("{login}/items")]
    public string SequreUserItemsGet([FromRoute] string login, [FromQuery] string password)
    {
        var user = _database.FindUser(login, password);
        if (user == null)
            return Serialize(BasicResponse.Failed);

        var items = from item in _database.Items
            where user.Id == item.UserId
            select item.ItemId;

        return Serialize(new UserItemsResponse
        {
            Status = Ok,
            SelectedAnimationsId = 1,
            SelectedCheckersId = 1,
            Items = items.ToArray()
        }); ;
    }

    [HttpPut("{login}/items")]
    public string SequreUserItemsPut([FromRoute] string login, [FromBody] JsonElement json)
    {
        var request = Deserialize<UserItemsUpdateRequest>(json.ToString());
        return Serialize(new UserItemsResponse
        {
            Status = Ok,
            SelectedAnimationsId = 1,
            SelectedCheckersId = 1,
            Items = new int[] { 1, 2, 3 }
        });
    }

    [HttpGet("{login}/achievements")]
    public string UserAchievementsGet([FromRoute] string login)
    {
        var user = _database.FindUser(login);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        var achievements = from a in _database.Achievements
            where a.UserId == user.Id
            select a.Id;
        return Serialize(new UserAchievementsGetResponse
        {
            Status = Ok,
            Achievements = achievements.ToArray()
        });
    }

    [HttpGet("{login}/friends")]
    public string UserFriendsGet([FromRoute] string login, [FromQuery] string password)
    {
        var user = _database.FindUser(login, password);
        if (user == null)
            return Serialize(BasicResponse.Failed);

        var result = from uf in _database.Friends
            where uf.UserId == user.Id
            join u in _database.Users on uf.FriendId equals u.Id
            select u.Login;

        return Serialize(new UserFriendsResponse
        {
            Status = Ok,
            Friends = result.ToArray()
        });
    }

    [HttpPut("{login}/friends")]
    public string UserFriendsPut([FromRoute] string login, [FromBody] JsonElement json)
    {
        var request = Deserialize<UserFriendsUpdateRequest>(json.ToString());
        return Serialize(new UserFriendsResponse
        {
            Status = Ok,
            Friends = new string[] { "biba", "boba" }
        }); ;
    }

    [HttpGet("{login}/games")]
    public string UserGamesGet([FromRoute] string login)
    {
        return Serialize(new UserGamesGetResponse
        {
            Status = Ok,
            Games = new[] { 1, 2, 3 }
        });
    }
}