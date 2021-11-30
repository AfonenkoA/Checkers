using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;


public class ControllerBase
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    protected static IActionResult Json(object obj) => new JsonResult(obj, Options);
    protected static readonly IActionResult OkResult = new OkResult();
    protected static readonly IActionResult BadRequestResult = new BadRequestResult();
}