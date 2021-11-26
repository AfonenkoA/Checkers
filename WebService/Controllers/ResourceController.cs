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
    public ResourceController(RepositoryFactory factory)
    {
        _repository = factory.GetRepository<ResourceRepository>();
    }
    private readonly IResourceRepository _repository;
    [HttpPost]
    public IActionResult UploadFile([FromQuery] Credential credential,[FromBody] byte[] picture, [FromQuery] string ext)
    {
        return new JsonResult(_repository.CreateFile(credential,picture,ext));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetFile([FromRoute] int id)
    {
        var (pic, ext) = _repository.GetFile(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}
