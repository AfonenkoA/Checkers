using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using WebService.Repository.Interface;
using WebService.Repository.MSSqlImplementation;
using static ApiContract.Action.ForumApiAction;
using static ApiContract.Route;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + ForumRoute)]
public class ForumController : ControllerBase
{
    public ForumController(RepositoryFactory factory) => _repository = factory.Get<ForumRepository>();

    private readonly IForumRepository _repository;
    [HttpPost]
    public IActionResult CreatePost([FromQuery] Credential credential, [FromBody] PostCreationData post) =>
        _repository.CreatePost(credential, post) ? OkResult : BadRequestResult;

    [HttpPut("{id:int}")]
    public IActionResult ActionHandler([FromQuery] Credential credential, [FromRoute] int id, [FromQuery] string action, [FromBody] string val) =>
        action switch
        {
            UpdatePostContentValue => UpdatePostContent(credential, id, val),
            UpdatePostPictureValue => UpdatePostPicture(credential, id, int.Parse(val)),
            UpdatePostTitleValue => UpdatePostTitle(credential, id, val),
            _ => BadRequestResult
        };

    private IActionResult UpdatePostContent(Credential credential, int id, string content) =>
        _repository.UpdateContent(credential, id, content) ? OkResult : BadRequestResult;

    private IActionResult UpdatePostPicture(Credential credential, int id, int picture) =>
        _repository.UpdatePictureId(credential, id, picture) ? OkResult : BadRequestResult;

    private IActionResult UpdatePostTitle(Credential credential, int id, string title) =>
        _repository.UpdateTitle(credential, id, title) ? OkResult : BadRequestResult;

    [HttpDelete("{id:int}")]
    public IActionResult DeletePost([FromQuery] Credential credential, [FromRoute] int id) =>
        BadRequestResult;

    [HttpGet("{id:int}")]
    public IActionResult GetPost(int id) => Json(_repository.GetPost(id));

    [HttpGet]
    public IActionResult GetPosts() => Json(_repository.GetPosts());
}