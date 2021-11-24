using System;
using System.Linq;
using System.Text.Json;
using Checkers.Data.Old;
using Checkers.Transmission;
using Microsoft.AspNetCore.Mvc;
using static System.Text.Json.JsonSerializer;
using User = Checkers.Data.Old.User;

namespace WebService.Controllers;

[Route("api/user")]
[ApiController]
public class OldUserController : ControllerBase
{
    private static readonly GameDatabase database = new();

    [HttpGet("{login}")]
    public string SimpleUserGet([FromRoute] string login)
    {
        var user = database.FindUser(login);
        if (user == null)
            return Serialize(BasicResponse.Failed);
        return Serialize(
            new UserGetResponse()
            {
                Status = ResponseStatus.OK,
                Info = new UserInfo()
                {
                    Nick = user.Nick,
                    Raiting = user.Rating,
                    PictureID = user.PictureId,
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
                new UserInfoResponse()
                {
                    Status = ResponseStatus.OK,
                    Info = new UserInfo()
                    {
                        Nick = login,
                        LastActivity = DateTime.Now,
                        PictureID = 1,
                        Raiting = 1000
                    },
                    Email = login + "@example.com",
                }),
            "authorize" => Serialize(new UserAuthorizationResponse() { Status = ResponseStatus.OK }),
            _ => Serialize(BasicResponse.Failed),
        };
    }

    [HttpPost]
    public string SequreUserPost([FromBody] JsonElement json)
    {
        UserUpdateRequest request = Deserialize<UserUpdateRequest>(json.ToString());
        return Serialize(
            new UserInfoResponse()
            {
                Status = ResponseStatus.OK,
                Email = request.Login + "@example.com",
                Info = new UserInfo()
                {
                    Nick = request.Login,
                    LastActivity = DateTime.Now,
                    PictureID = 1,
                    Raiting = 1000
                }
            });
    }

    [HttpDelete]
    public string Delete([FromQuery] string login, [FromQuery] string password)
    {
        return Serialize(new UserDeleteResponse() { Status = ResponseStatus.OK });
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

        return Serialize(new UserItemsResponse()
        {
            Status = ResponseStatus.OK,
            SelectedAnimationsID = 1,
            SelectedCheckersID = 1,
            Items = items.ToArray()
        }); ;
    }

    [HttpPut("{login}/items")]
    public string SequreUserItemsPut([FromRoute] string login, [FromBody] JsonElement json)
    {
        UserItemsUpdateRequest request = Deserialize<UserItemsUpdateRequest>(json.ToString());
        return Serialize(new UserItemsResponse()
        {
            Status = ResponseStatus.OK,
            SelectedAnimationsID = 1,
            SelectedCheckersID = 1,
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
        return Serialize(new UserAchievementsGetResponse()
        {
            Status = ResponseStatus.OK,
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

        return Serialize(new UserFriendsResponse()
        {
            Status = ResponseStatus.OK,
            Friends = result.ToArray()
        });
    }

    [HttpPut("{login}/friends")]
    public string UserFriendsPut([FromRoute] string login, [FromBody] JsonElement json)
    {
        UserFriendsUpdateRequest request = Deserialize<UserFriendsUpdateRequest>(json.ToString());
        return Serialize(new UserFriendsResponse()
        {
            Status = ResponseStatus.OK,
            Friends = new string[] { "biba", "boba" }
        }); ;
    }

    [HttpGet("{login}/games")]
    public string UserGamesGet([FromRoute] string login)
    {
        return Serialize(new UserGamesGetResponse()
        {
            Status = ResponseStatus.OK,
            Games = new int[] { 1, 2, 3 }
        });
    }
}