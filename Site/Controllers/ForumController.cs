using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.Post;
using Site.Repository.Interface;

namespace Site.Controllers;

public sealed class ForumController : Controller
{
    private readonly IForumRepository _repository;

    public ForumController(IForumRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index(Credential c)
    {
        var (success, data) = await _repository.GetPosts();
        var model = new Identified<IEnumerable<PostPreview>>(c, data);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Post(Credential c, int id)
    {
        if (!c.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "Index", callerController = "Forum"});
        var (success, data) = await _repository.GetPost(c, id);
        var model = new Identified<PostView>(c, data);
        return success ? View(model) : View("Error");
    }
}