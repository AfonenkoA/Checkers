using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using static System.Text.Json.JsonSerializer;
using Checkers.Transmission;

namespace WevService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("{login}")]
        public string SimpleUserGet([FromRoute] string login)
        {
            Console.WriteLine(login);
            return Serialize(
                new UserGetResponse()
                {
                    Status = ResponseStatus.OK,
                    Info = new UserInfo()
                    {
                        Nick = login,
                        LastActivity = DateTime.Now,
                        PictureID = 1,
                        Raiting = 1000
                    }
                });
        }

        [HttpGet]
        public string SequreUserGet([FromQuery] string login, [FromQuery] string password, [FromQuery] string action)
        {
            switch (action)
            {
                case "info":
                    return Serialize(
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
                });
                case "authorize":
                    return Serialize(new UserAuthorizationResponse() { Status = ResponseStatus.OK });
                default:
                    return Serialize(new Response() { Status = ResponseStatus.FAILED });
            }

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
            return Serialize(new UserItemsResponse()
            {
                Status = ResponseStatus.OK,
                SelectedAnimationsID = 1,
                SelectedCheckersID = 1,
                Items = new int[] { 1, 2, 3 }
            });
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
            return Serialize(new UserAchievementsGetResponse()
            {
                Status = ResponseStatus.OK,
                Achievements = new int[] { 1, 2, 3 }
            });
        }

        [HttpGet("{login}/friends")]
        public string UserFriendsGet([FromRoute] string login, [FromQuery] string password)
        {
            return Serialize(new UserFriendsResponse()
            {
                Status = ResponseStatus.OK,
                Friends = new string[] { "friend 1", "friend 2", "friend 3" }
            });
        }

        [HttpPut("{login}/friends")]
        public string UserFriendsPut([FromRoute] string login, [FromBody] JsonElement json)
        {
            UserFriendsUpdateRequest request = Deserialize<UserFriendsUpdateRequest>(json.ToString());
            return Serialize(new UserFriendsResponse()
            {
                Status = ResponseStatus.OK,
                Friends = new string[] {"friend 1","friend 2","friend 3" }
            });
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
}
