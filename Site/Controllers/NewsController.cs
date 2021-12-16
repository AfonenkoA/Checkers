using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;
using Site.Data.Models.Article;
using Site.Repository.Interface;

namespace Site.Controllers;

public sealed class NewsController : Controller
{
    private readonly INewsRepository _repository;
    public NewsController(INewsRepository repository)
    {
        _repository = repository;
    }
    public async Task<IActionResult> Index(Credential c)
    {
        var (success, data) = await _repository.GetNews();
        var news = data.Select(article => new Identified<ArticlePreview>(c, article)).Cast<IIdentified<ArticlePreview>>().ToList();
        var model = new Identified<IEnumerable<IIdentified<ArticlePreview>>>(c, news);
        return success ? View(model) : View("Error");
    }

    public async Task<IActionResult> Article(Credential c,int id)
    {
        var (success, article) = await _repository.GetArticle(id);
        var model = new Identified<ArticleView>(c, article);
        return success ? View(model) : View("Error");
    }
}