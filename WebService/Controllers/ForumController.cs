using System.Linq;
using Checkers.Api.WebImplementation;
using Checkers.Data;
using Checkers.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + WebApiBase.ForumRoute)]
public class ForumController
{
    [HttpPost]
    public IActionResult CreatePost([FromQuery] Credential credential, [FromBody] PostCreationData post)
    {
        return new OkResult();
    }

    [HttpPost("{id:int}")]
    public IActionResult UpdatePost([FromQuery] Credential credential, [FromBody] PostCreationData post)
    {
        return new OkResult();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePost([FromQuery] Credential credential, [FromRoute] int id)
    {
        return new OkResult();
    }

    [HttpGet("{id:int}")]
    public JsonResult GetPost(int id)
    {
        return new JsonResult(new Post());
    }

    public IActionResult CommentPost(int postId, string comment)
    {
        return new OkResult();
    }


    public JsonResult GetPosts()
    {
        return new JsonResult(Enumerable.Repeat(new PostInfo(),10));
    }
}