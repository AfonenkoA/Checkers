using System;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;
using static Checkers.Api.WebImplementation.WebApiBase;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + ChatRoute)]
public class ChatController : ControllerBase
{
    public ChatController(RepositoryFactory factory) => _repository = factory.Get<ChatRepository>();

    private readonly IChatRepository _repository;
    [HttpGet("{id:int}")]
    public IActionResult GetMessages([FromQuery] Credential credential, [FromRoute] int id, [FromQuery] DateTime from) =>
        Json(_repository.GetMessages(credential, id, from));

    [HttpPost("{id:int}")]
    public IActionResult SendMessage([FromQuery] Credential credential, [FromRoute] int id, [FromBody] string message) =>
        _repository.CreateMessage(credential, id, message) ? OkResult : BadRequestResult;
}
