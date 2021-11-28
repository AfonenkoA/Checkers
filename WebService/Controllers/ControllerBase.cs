using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;


public class ControllerBase
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected static IActionResult Json(object obj) => new JsonResult(obj, Options);
    protected static readonly IActionResult OkResult = new OkResult();
    protected static readonly IActionResult BadRequestResult = new BadRequestResult();
}