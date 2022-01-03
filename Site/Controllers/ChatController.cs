using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.UserIdentity;
using Site.Service.Interface;

namespace Site.Controllers;

public sealed class ChatController : ControllerBase
{
    private readonly IChatService _chatRepository;

    public ChatController(IChatService chatRepository, IUserService user) : base(user)
    {
        _chatRepository = chatRepository;
    }

    private static object ChatValues(IIdentity i, int id) =>
        new
        {
            login = i.Login,
            password = i.Password,
            type = i.Type,
            id = id
        };

    public async Task<IActionResult> CommonChat(Identity i)
    {
        if (!i.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "CommonChat", callerController = "Chat" });
        var (success, id) = await _chatRepository.GetCommonChatId(i);
        if (!success) return View("Error");
        return RedirectToAction("Chat", ChatValues(i,id));
    }

    public async Task<IActionResult> Chat(Identity i, int id)
    {
        var (success, data) = await _chatRepository.GetMessages(i, id);
        var model = new Identified<Chat>(i, data);
        return success ? View(model) : View("Error");
    }

    [HttpPost]
    public async Task<IActionResult> Send(Identity i, int id, string message)
    {
        var success = await _chatRepository.SendMessage(i, id, message);
        return success ? RedirectToAction("Chat", "Chat", ChatValues(i, id)) : View("Error");
    }
}