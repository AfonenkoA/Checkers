using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;


public class Controller
{
    protected static readonly IActionResult OkResult = new OkResult();
    protected static readonly IActionResult BadRequestResult = new BadRequestResult();
}