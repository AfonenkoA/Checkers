using System;
using System.Linq;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.ChatRoute)]
public class ChatController
{
    [HttpGet("{id:int}")]
    public JsonResult GetMessages([FromRoute] int id, [FromQuery] DateTime from)
    {
        return new JsonResult(Enumerable.Repeat(new Message(),10));
    }

    [HttpPost("{id:int}")]
    public IActionResult SendMessage([FromQuery] Credential credential,[FromRoute] int id,[FromBody] string message)
    {
        return new OkResult();
    }
}
