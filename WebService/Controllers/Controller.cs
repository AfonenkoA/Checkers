using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

public class Controller
{
    public static readonly IActionResult OkResult = new OkResult();
    public static readonly IActionResult BadRequestResult = new BadRequestResult();
}