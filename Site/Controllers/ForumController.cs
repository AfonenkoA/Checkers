using Microsoft.AspNetCore.Mvc;
using Site.Data.Models.Post;
using Site.Data.Models.UserIdentity;
using Site.Repository.Interface;

namespace Site.Controllers;

public sealed class ForumController : Controller
{
    private readonly IForumRepository _repository;

    public ForumController(IForumRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index(Identity i)
    {
        var (success, data) = await _repository.GetPosts();
        var model = new Identified<IEnumerable<PostPreview>>(i, data);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Post(Identity i, int id)
    {
        if (i.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "Index", callerController = "Forum" });
        var (success, data) = await _repository.GetPost(i, id);
        var model = new Identified<PostView>(i, data);
        return success ? View(model) : View("Error");
    }

    public sealed class PostCreation
    {
        public IFormFile? Picture { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public IActionResult CreationPage(Identity i)
    {
        if (!i.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "CreationPage", callerController = "Forum" });
        return View(i);
    }

    [HttpPost]
    public Task<IActionResult> Create(PostCreation p)
    {
        return null;
    }
}