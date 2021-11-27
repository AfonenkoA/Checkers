using System;
using System.Text.Json;
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

    public ChatController(Repository.Factory factory)
    {
        _repository = factory.Get<ChatRepository>();
    }

    private readonly IChatRepository _repository;
    [HttpGet("{id:int}")]
    public IActionResult GetMessages([FromQuery] Credential credential,[FromRoute] int id, [FromQuery] DateTime from)
    {
        return Json(_repository.GetMessages(credential,id,from));
    }

    [HttpPost("{id:int}")]
    public IActionResult SendMessage([FromQuery] Credential credential,[FromRoute] int id,[FromBody] string message)
    {
        return _repository.CreateMessage(credential,id,message) ? OkResult : BadRequestResult;
    }
}
