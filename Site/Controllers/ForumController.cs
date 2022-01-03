using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.Post;
using Site.Data.Models.UserIdentity;
using Site.Service.Interface;

namespace Site.Controllers;

public sealed class ForumController : ControllerBase
{
    private readonly IForumService _repository;

    public ForumController(IForumService repository, IUserService user) : base(user)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index(Identity i)
    {
        var (success, data) = await _repository.GetPosts();
        var model = new Identified<IEnumerable<Preview>>(i, data);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Post(Identity i, int id)
    {
        if (!i.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "Index", callerController = "Forum" });
        var (success, data) = await _repository.GetPost(i, id);
        var model = new Identified<VisualPost>(i, data);
        return success ? View(model) : View("Error");
    }


    public IActionResult CreationPage(Identity i)
    {
        if (!i.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "CreationPage", callerController = "Forum" });
        return View(i);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreationData p)
    {
        if (p.Login == null) return View("Error");
        var login = p.Login;
        if (p.Password == null) return View("Error");
        var password = p.Password;
        var (s, identity) = await Authorize(login, password);
        if (!s) return View("Error");
        var success = await _repository.Create(p);
        return success ? RedirectToAction("Index", "Forum", IdentityValues(identity)) : View("Error");
    }

    public async Task<IActionResult> UpdateTitle(Identity i, int id, string title)
    {
        var success = await _repository.UpdateTitle(i, id, title);
        return View("ModificationResult",
            new Identified<ModificationResult>(i, 
                new ModificationResult("Forum", "Index", success)));
    }

    public async Task<IActionResult> UpdateContent(Identity i, int id, string content)
    {
        var success = await _repository.UpdateContent(i, id, content);
        return View("ModificationResult",
            new Identified<ModificationResult>(i,
                new ModificationResult("Forum", "Index", success)));
    }

    public async Task<IActionResult> UpdatePicture(PictureUpdateData p)
    {
        var success = await _repository.UpdatePicture(p);
        return View("ModificationResult",
            new Identified<ModificationResult>(p,
                new ModificationResult("Forum", "Index", success)));
    }
}