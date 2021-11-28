using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.NewsRoute)]
public class NewsController : Controller
{
    public NewsController(RepositoryFactory factory)
    {
        _repository = factory.Get<NewsRepository>();
    }

    private readonly INewsRepository _repository;
    [HttpPost]
    public IActionResult CreateArticle([FromQuery] Credential credential, [FromBody] ArticleCreationData article)
    {

        return _repository.CreateArticle(credential,article) ? OkResult : BadRequestResult;
    }

    [HttpPost("{id:int}")]
    public IActionResult UpdateArticle([FromQuery] Credential credential, [FromQuery] ArticleCreationData article)
    {
        return BadRequestResult;
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteArticle([FromQuery] Credential credential, [FromRoute] int id)
    {
        return BadRequestResult;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetArticle([FromRoute] int id)
    {
        return Json(_repository.GetArticle(id));
    }

    [HttpGet]
    public IActionResult GetNews()
    {
        return Json(_repository.GetNews());
    }
}
