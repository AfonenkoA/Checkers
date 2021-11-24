using Checkers.Api.WebImplementation;
using Checkers.Data;
using Checkers.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[ApiController]
[Route("api/"+WebApiBase.ResourceRoute)]
public class ResourceController
{
    [HttpPost]
    public IActionResult UploadFile([FromQuery] Credential credential,[FromBody] byte[] picture, [FromQuery] string ext)
    {
        return new OkResult();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetFile([FromRoute] int id)
    {
        return new PhysicalFileResult(@"W:\IT\C#\Checkers\WebService\1.png", "image/png");
    }
}
