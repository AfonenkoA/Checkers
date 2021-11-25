using System.IO;
using System.Linq;
using System.Resources;
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
    private static readonly IResourceRepository Repository = new ResourceRepository();
    [HttpPost]
    public IActionResult UploadFile([FromQuery] Credential credential,[FromBody] byte[] picture, [FromQuery] string ext)
    {
        return new JsonResult(Repository.CreateFile(credential,picture,ext));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetFile([FromRoute] int id)
    {
        var (pic, ext) = Repository.GetFile(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}
