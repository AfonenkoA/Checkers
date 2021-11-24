using System.Linq;
using Checkers.Api.WebImplementation;
using Checkers.Data;
using Checkers.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.NewsRoute)]
public class NewsController
{
    [HttpPost]
    public IActionResult CreateArticle([FromQuery] Credential credential, [FromBody] ArticleCreationData article)
    {
        return new OkResult();
    }

    [HttpPost("{id:int}")]
    public IActionResult UpdateArticle([FromQuery] Credential credential, [FromQuery] ArticleCreationData article)
    {
        return new OkResult();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteArticle([FromQuery] Credential credential, [FromRoute] int id)
    {
        return new OkResult();
    }

    [HttpGet("{id:int}")]
    public JsonResult GetArticle([FromRoute] int id)
    {
        return new JsonResult(new Article());
    }

    [HttpGet]
    public JsonResult GetNews()
    {
        return new JsonResult(Enumerable.Repeat(new Article(),10));
    }
}
