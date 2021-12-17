using Microsoft.AspNetCore.Mvc;
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
        var model = new Identified<IEnumerable<ArticlePreview>>(i, data);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Article(Identity i,int id)
    {
        var (success, article) = await _repository.GetArticle(id);
        var model = new Identified<ArticleView>(i, article);
        return success ? View(model) : View("Error");
    }
}