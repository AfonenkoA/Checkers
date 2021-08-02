using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using Checkers.Transmission;

namespace WevService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);
        private T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);

        [HttpGet("{login}")]
        public string SimpleUserGet([FromRoute] string login)
        {
            return JsonSerializer.Serialize(
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
        public string SequreUserGet([FromQuery] string login, [FromQuery] string password)
        {
            return JsonSerializer.Serialize(
                new UserLoginResponse()
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
                    Login = login,
                    Password = password
                });
        }

        [HttpPost]
        public string SequreUserPost([FromBody] JsonElement json)
        {
            UserUpdateRequest request = Deserialize<UserUpdateRequest>(json.ToString());
            return Serialize(
                new UserUpdateResponse()
                {
                    Status = ResponseStatus.OK,
                    Email = request.Login + "@example.com",
                    Password = request.Password,
                    Login = request.Login,
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
                Login = login,
                Password = password,
                SelectedAnimationsID = 1,
                SelectedCheckersID = 1,
                Items = new int[] { 1, 2, 3 }
            });
        }

        [HttpPut("{login}/items")]
        public string SequreUserItemsPut([FromRoute] string login, [FromBody] JsonElement json)
        {
            UserItemsUpdateRequest request = Deserialize<UserItemsUpdateRequest>(json.ToString());
            return Serialize(new UserItemsUpdateResponse()
            {
                Status = ResponseStatus.OK,
                Login = login,
                Password = request.Password,
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
            return Serialize(new UserFriendsGetResponse()
            {
                Status = ResponseStatus.OK,
                Login = login,
                Password = password,
                Friends = new int[] { 1, 2, 3 }
            });
        }

        [HttpPut("{login}/friends")]
        public string UserFriendsPut([FromRoute] string login, [FromBody] JsonElement json)
        {
            UserFriendsUpdateRequest request = Deserialize<UserFriendsUpdateRequest>(json.ToString());
            return Serialize(new UserFriendsUpdateResponse()
            {
                Status = ResponseStatus.OK,
                Login = login,
                Password = request.Password,
                Friends = new int[] { 1, 2, 3 }
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
