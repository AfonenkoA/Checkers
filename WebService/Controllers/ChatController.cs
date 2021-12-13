using System;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using WebService.Repository.Interface;
using WebService.Repository.MSSqlImplementation;
using static ApiContract.Route;

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

    [HttpGet, Route("public")]
    public IActionResult GetPublicChat([FromQuery] Credential credential) =>
        Json(_repository.GetCommonChatId(credential));

    [HttpPost("{id:int}")]
    public IActionResult SendMessage([FromQuery] Credential credential, [FromRoute] int id, [FromBody] string message) =>
        _repository.CreateMessage(credential, id, message) ? OkResult : BadRequestResult;
}
