using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + WebApiBase.ForumRoute)]
public class ForumController : Controller
{
    public ForumController(RepositoryFactory factory)
    {
        _repository = factory.GetRepository<ForumRepository>();
    }

    private readonly IForumRepository _repository;
    [HttpPost]
    public IActionResult CreatePost([FromQuery] Credential credential, [FromBody] PostCreationData post)
    {
        return _repository.CreatePost(credential, post) ? OkResult : BadRequestResult;
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePost([FromQuery] Credential credential, [FromRoute] int id)
    {
        return BadRequestResult;
    }

    [HttpGet("{id:int}")]
    public JsonResult GetPost(int id)
    {
        return new JsonResult(_repository.GetPost(id));
    }

    [HttpPost("{id:int}")]
    public IActionResult CommentPost([FromQuery]Credential credential,[FromRoute]int postId,[FromBody] string comment)
    {
        return _repository.CommentPost(credential,postId,comment) ? OkResult : BadRequestResult;
    }


    public JsonResult GetPosts()
    {
        return new JsonResult(_repository.GetPosts());
    }
}