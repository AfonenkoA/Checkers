using Microsoft.AspNetCore.Mvc;
using static Common.CommunicationProtocol;

namespace WebService.Controllers;


public class ControllerBase
{

    protected static IActionResult Json(object obj) => new JsonResult(obj, Options);
    protected static readonly IActionResult OkResult = new OkResult();
    protected static readonly IActionResult BadRequestResult = new BadRequestResult();
}