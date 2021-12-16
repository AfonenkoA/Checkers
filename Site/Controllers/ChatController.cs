using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Repository.Interface;

namespace Site.Controllers;

public sealed class ChatController : Controller
{
    public readonly IChatRepository _repository;

    public ChatController(IChatRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> CommonChat(Credential c)
    {
        if (!c.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "CommonChat", callerController = "Chat" });
        var (success, id) = await _repository.GetCommonChatId(c);
        if (!success) return View("Error");
        return RedirectToAction("Chat",
            new { login = c.Login, password = c.Password, id = id });
    }

    public async Task<IActionResult> Chat(Credential c, int id)
    {
        var (success, data) = await _repository.GetMessages(c, id);
        var model = new Identified<Chat>(c, data);
        return success ? View(model) : View("Error");
    }

    [HttpPost]
    public async Task<IActionResult> Send(Credential c, int id, string message)
    {
        var success = await _repository.SendMessage(c, id, message);
        return success ? RedirectToAction("Chat", new { login = c.Login, password = c.Password, id = id }) :
            View("Error");
    }
}