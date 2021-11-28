using System.IO;
using System.Linq;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Microsoft.AspNetCore.Mvc;
using static Checkers.Api.WebImplementation.WebApiBase;

namespace WebService.Controllers;

[Route("api/" + ItemRoute)]
[ApiController]
public class ItemController : ControllerBase
{
    public ItemController(RepositoryFactory factory) => _repository = factory.Get<ItemRepository>();

    private readonly IItemRepository _repository;
    [HttpGet]
    public IActionResult GetItems() => Json(_repository.GetItems());

    [HttpGet("{id:int}")]
    public IActionResult GetItemInfo(int id)
    {
        var result = _repository.GetItemInfo(id);
        return result.IsValid ? Json(result) : BadRequestResult;
    }

    [HttpGet("{id:int}/img")]
    public IActionResult GetItemImage(int id)
    {
        var (pic, ext) = _repository.GetItemImage(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}