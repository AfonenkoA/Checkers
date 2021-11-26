using System;
using System.Linq;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.ChatRoute)]
public class ChatController : Controller
{
    private static readonly IChatRepository Repository = new ChatRepository();
    [HttpGet("{id:int}")]
    public JsonResult GetMessages([FromQuery] Credential credential,[FromRoute] int id, [FromQuery] DateTime from)
    {
        return new JsonResult(Repository.GetMessages(credential,id,from));
    }

    [HttpPost("{id:int}")]
    public IActionResult SendMessage([FromQuery] Credential credential,[FromRoute] int id,[FromBody] string message)
    {
        return Repository.CreateMessage(credential,id,message) ? OkResult : BadRequestResult;
    }
}
