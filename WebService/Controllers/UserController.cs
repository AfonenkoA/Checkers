using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace WevService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{username}")]
        public string SimpleUserGet(string username)
        {
            return username;
        }

        [HttpGet]
        public string SequreUserGet([FromQuery] string login, [FromQuery] string password)
        {
            return password;
        }

        [HttpPost]
        public JsonResult SequreUserPost([FromBody] JsonElement json)
        {
            Console.WriteLine(json.ToString());
            return new JsonResult(json);
        }

        [HttpDelete]
        public string Delete([FromQuery] string login, [FromQuery] string password)
        {
            return null;
        }

        [HttpGet("{login}/items")]
        public string SequreUserItemsGet([FromRoute] string login, [FromQuery] string password)
        {
            return null;
        }

        [HttpPut("{login}/items")]
        public string SequreUserItemsPut([FromRoute] string login, [FromBody] JsonElement json)
        {
            return null;
        }

        [HttpGet("{{login}/achievements}")]
        public string UserAchievementsGet([FromRoute] string login)
        {
            return null;
        }

        [HttpGet("{login}/friends")]
        public string UserFriendsGet([FromRoute] string login, [FromQuery] string password)
        {
            return null;
        }

        [HttpPut("{login}/friends")]
        public string UserFriendsPut([FromRoute] string login, [FromBody] JsonElement json)
        {
            return null;
        }

        [HttpGet("{login}/games")]
        public string UserGamesGet([FromRoute] string login)
        {
            return null;       
        }

    }
}
