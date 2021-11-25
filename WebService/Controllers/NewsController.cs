﻿using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.NewsRoute)]
public class NewsController : Controller
{
    private static readonly INewsRepository Repository = new NewsRepository();
    [HttpPost]
    public IActionResult CreateArticle([FromQuery] Credential credential, [FromBody] ArticleCreationData article)
    {

        return Repository.CreateArticle(credential,article) ? OkResult : BadRequestResult;
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
    public JsonResult GetArticle([FromRoute] int id)
    {
        return new JsonResult(Repository.GetArticle(id));
    }

    [HttpGet]
    public JsonResult GetNews()
    {
        return new JsonResult(Repository.GetNews());
    }
}
