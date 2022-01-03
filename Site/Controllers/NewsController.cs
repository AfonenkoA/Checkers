using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.Article;
using Site.Data.Models.UserIdentity;
using Site.Service.Interface;

namespace Site.Controllers;

public sealed class NewsController : ControllerBase
{
    private readonly INewsService _repository;
    public NewsController(INewsService repository, IUserService user) : base(user)
    {
        _repository = repository;
    }
    public async Task<IActionResult> Index(Identity i)
    {
        var (success, data) = await _repository.GetNews();
        var model = new Identified<IEnumerable<Preview>>(i, data);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Article(Identity i, int id)
    {
        var (success, article) = await _repository.GetArticle(id);
        var model = new Identified<VisualArticle>(i, article);
        return success ? View(model) : View("Error");
    }

    public IActionResult CreationPage(Identity i)
    {
        if (!i.IsValid) return RedirectToAction("Login", "Authorization",
            new { callerAction = "CreationPage", callerController = "Forum" });
        return View(i);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreationData data)
    {
        if (data.Login == null) return View("Error");
        var login = data.Login;
        if (data.Password == null) return View("Error");
        var password = data.Password;
        var (s, identity) = await Authorize(login, password);
        if (!s) return View("Error");
        var success = await _repository.CreateArticle(data);
        return success ? RedirectToAction("Index", "News", IdentityValues(identity)) : View("Error");
    }

    public async Task<IActionResult> UpdateTitle(Identity i, int id, string title)
    {
        var success = await _repository.UpdateTitle(i, id, title);
        return View("ModificationResult",
            new Identified<ModificationResult>(i,
                new ModificationResult("News", "Index", success)));
    }

    public async Task<IActionResult> UpdateContent(Identity i, int id, string content)
    {
        var success = await _repository.UpdateContent(i, id, content);
        return View("ModificationResult",
            new Identified<ModificationResult>(i,
                new ModificationResult("News", "Index", success)));
    }

    public async Task<IActionResult> UpdateAbstract(Identity i, int id, string @abstract)
    {
        var success = await _repository.UpdateAbstract(i, id, @abstract);
        return View("ModificationResult",
            new Identified<ModificationResult>(i,
                new ModificationResult("News", "Index", success)));
    }

    public async Task<IActionResult> UpdatePicture(PictureUpdateData pic)
    {
        var success = await _repository.UpdatePicture(pic);
        return View("ModificationResult",
            new Identified<ModificationResult>(pic,
                new ModificationResult("News", "Index", success)));
    }
}