using System.IO;
using System.Linq;
using Checkers.Api.WebImplementation;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("api/"+WebApiBase.ItemRoute)]
[ApiController]
public class ItemController : Controller
{
    private static readonly IItemRepository Repository = ItemRepository.Instance;
    [HttpGet]
    public JsonResult GetItems()
    {
        return new JsonResult(Repository.GetItems());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetItemInfo(int id)
    {
        var result = Repository.GetItemInfo(id);
        return result.IsValid ? new JsonResult(result) : BadRequestResult;
    }

    [HttpGet("{id:int}/img")]
    public IActionResult GetItemImage(int id)
    {
        var (pic, ext) = Repository.GetItemImage(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}