using Microsoft.AspNetCore.Mvc;
using static System.Text.Json.JsonSerializer;
using Checkers.Transmission;
using System;

namespace WevService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet("{id}")]
        public string GameGet([FromRoute] int id)
        {
            return Serialize(new GameGetRespose()
            {
                Status = ResponseStatus.OK,
                WinnerID = 1,
                Player1ID = 1,
                Player2ID = 1,
                ID = id,
                Player1RaitingChange = 10,
                Player2RaitingChange = -10,
                Player1CheckersID = 1,
                Player2CheckersID = 1,
                Player1AnimationsID = 1,
                Player2AnimationsID = 1,
                StartTime = DateTime.Now,
                Actions = new GameAction[]
                {
                    new GameAction()
                    {
                        ActionNumber = 1,
                        ActorID = 1,
                        ActionDescription = "desc",
                        ActionTime = new TimeSpan(0,1,0)
                    }
                }
            });
        }
    }
}
