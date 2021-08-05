using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using static System.Text.Json.JsonSerializer;
using Checkers.Transmission;
using Checkers.Data;
using System.Linq;

namespace WevService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly Database database = new();

        private User FindUser(string login, string password)
        {
            var result = from u in database.Users
                         where u.Login == login && u.Password == password
                         select u;
            return result.Any() ? result.First() : null;
        }

        private User FindUser(string login)
        {
            var result = from u in database.Users
                         where u.Login == login
                         select u;
            return result.Any() ? result.First() : null;
        }

        [HttpGet("{login}")]
        public string SimpleUserGet([FromRoute] string login)
        {
            User user = FindUser(login);
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
            User user = FindUser(login, password);
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
            User user = FindUser(login, password);
            if(user == null)
                return Serialize(BasicResponse.Failed);

            var items = from item in database.Items
                               where user.Id == item.UserId
                               select item.Id;

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
            User user = FindUser(login);
            if(user == null)
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
                Friends = new string[] { "friend 1", "friend 2", "friend 3" }
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
