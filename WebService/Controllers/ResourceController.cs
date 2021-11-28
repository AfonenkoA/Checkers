using System;
using System.IO;
using System.Linq;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.ResourceRoute)]
public class ResourceController : Controller
{
    public ResourceController(RepositoryFactory factory) => _repository = factory.Get<ResourceRepository>();
    private readonly IResourceRepository _repository;
    [HttpPost]
    public IActionResult UploadFile([FromQuery] Credential credential,[FromBody] string picture, [FromQuery] string ext) => 
        Json(_repository.CreateFile(credential,Convert.FromBase64String(picture),ext));

    [HttpGet("{id:int}")]
    public IActionResult GetFile([FromRoute] int id)
    {
        var (pic, ext) = _repository.GetFile(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}
