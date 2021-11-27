﻿using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + WebApiBase.ForumRoute)]
public class ForumController : Controller
{
    public ForumController(Repository.Factory factory)
    {
        _repository = factory.Get<ForumRepository>();
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


    public JsonResult GetPosts()
    {
        return new JsonResult(_repository.GetPosts());
    }
}