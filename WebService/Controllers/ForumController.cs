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
    private static readonly IForumRepository Repository = new ForumRepository();
    [HttpPost]
    public IActionResult CreatePost([FromQuery] Credential credential, [FromBody] PostCreationData post)
    {
        return Repository.CreatePost(credential, post) ? OkResult : BadRequestResult;
    }

    [HttpPost("{id:int}")]
    public IActionResult UpdatePost([FromQuery] Credential credential, [FromBody] PostCreationData post)
    {
        return BadRequestResult;
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePost([FromQuery] Credential credential, [FromRoute] int id)
    {
        return BadRequestResult;
    }

    [HttpGet("{id:int}")]
    public JsonResult GetPost(int id)
    {
        return new JsonResult(Repository.GetPost(id));
    }

    [HttpPost("{id:int}")]
    public IActionResult CommentPost([FromRoute]int postId,[FromBody] string comment)
    {
        return BadRequestResult;
    }


    public JsonResult GetPosts()
    {
        return new JsonResult(Repository.GetPosts());
    }
}