using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;
using static Checkers.Api.Interface.Action.NewsApiAction;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + WebApiBase.NewsRoute)]
public class NewsController : Controller
{
    public NewsController(RepositoryFactory factory) => _repository = factory.Get<NewsRepository>();

    private readonly INewsRepository _repository;
    [HttpPost]
    public IActionResult CreateArticle([FromQuery] Credential credential, [FromBody] ArticleCreationData article) =>
        _repository.CreateArticle(credential, article) ? OkResult : BadRequestResult;


    [HttpPut("{id:int}")]
    public IActionResult ActionHandler([FromQuery] Credential credential, [FromRoute] int id, [FromQuery] string action, [FromBody] string val) =>
        action switch
        {
            UpdateArticleAbstractValue => UpdateArticleAbstract(credential, id, val),
            UpdateArticleContentValue => UpdateArticleContent(credential, id, val),
            UpdateArticlePictureIdValue => UpdateArticlePictureId(credential, id, int.Parse(val)),
            UpdateArticleTitleValue => UpdateArticleTitle(credential, id, val),
            _ => BadRequestResult
        };

    private IActionResult UpdateArticleAbstract(Credential credential, int id, string @abstract) =>
        _repository.UpdateAbstract(credential, id, @abstract) ? OkResult : BadRequestResult;

    private IActionResult UpdateArticleContent(Credential credential, int id, string content) =>
        _repository.UpdateContent(credential, id, content) ? OkResult : BadRequestResult;

    private IActionResult UpdateArticlePictureId(Credential credential, int id, int pictureId) =>
        _repository.UpdatePictureId(credential, id, pictureId) ? OkResult : BadRequestResult;

    private IActionResult UpdateArticleTitle(Credential credential, int id, string title) =>
        _repository.UpdateTitle(credential, id, title) ? OkResult : BadRequestResult;

    [HttpDelete("{id:int}")]
    public IActionResult DeleteArticle([FromQuery] Credential credential, [FromRoute] int id) =>
        BadRequestResult;

    [HttpGet("{id:int}")]
    public IActionResult GetArticle([FromRoute] int id) => Json(_repository.GetArticle(id));

    [HttpGet]
    public IActionResult GetNews() => Json(_repository.GetNews());
}
