using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.Article;
using Site.Data.Models.UserIdentity;
using Site.Repository.Interface;

namespace Site.Controllers;

public sealed class NewsController : Controller
{
    private readonly INewsRepository _repository;
    public NewsController(INewsRepository repository)
    {
        _repository = repository;
    }
    public async Task<IActionResult> Index(Identity i)
    {
        var (success, data) = await _repository.GetNews();
        var model = new Identified<IEnumerable<Preview>>(i, data);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Article(Identity i,int id)
    {
        var (success, article) = await _repository.GetArticle(id);
        var model = new Identified<VisualArticle>(i, article);
        return success ? View(model) : View("Error");
    }

    public void CreationPage()
    {}

    public void Create()
    { }

    public async Task<IActionResult> UpdateTitle(Identity i, int id, string title)
    {
        var success = await _repository.UpdateTitle(i, id, title);
        return View("ModificationResult",
            new Identified<ModificationResult>(i,
                new ModificationResult("News", "Index", success)));
    }

    public async Task<IActionResult> UpdateContent(Identity i, int id, string content)
    {
        var success = await _repository.UpdateAbstract(i, id, content);
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