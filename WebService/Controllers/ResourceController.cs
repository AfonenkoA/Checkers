using System;
using System.IO;
using System.Linq;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;
using static Checkers.Api.WebImplementation.WebApiBase;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + ResourceRoute)]
public class ResourceController : ControllerBase
{
    public ResourceController(RepositoryFactory factory) => _repository = factory.Get<ResourceRepository>();
    private readonly IResourceRepository _repository;
    [HttpPost]
    public IActionResult UploadFile([FromQuery] Credential credential, [FromBody] string picture, [FromQuery] string val) =>
        Json(_repository.CreateFile(credential, Convert.FromBase64String(picture), val));

    [HttpGet("{id:int}")]
    public IActionResult GetFile([FromRoute] int id)
    {
        var (pic, ext) = _repository.GetFile(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}
