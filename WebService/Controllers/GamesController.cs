using System;
using Checkers.Transmission;
using Microsoft.AspNetCore.Mvc;
using static System.Text.Json.JsonSerializer;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{

    [HttpGet("{id:int}")]
    public string GameGet([FromRoute] int id)
    {
        return Serialize(new GameGetRespose
        {
            Status = ResponseStatus.Ok,
            WinnerId = 1,
            Player1Id = 1,
            Player2Id = 1,
            Id = id,
            Player1RaitingChange = 10,
            Player2RaitingChange = -10,
            Player1CheckersId = 1,
            Player2CheckersId = 1,
            Player1AnimationsId = 1,
            Player2AnimationsId = 1,
            StartTime = DateTime.Now,
            Actions = new[]
            {
                new GameAction
                {
                    ActionNumber = 1,
                    ActorId = 1,
                    ActionDescription = "desc",
                    ActionTime = new TimeSpan(0,1,0)
                }
            }
        });
    }
}