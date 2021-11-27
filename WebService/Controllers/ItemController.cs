﻿using System.IO;
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
    public ItemController(Repository.Factory factory)
    {
        _repository = factory.Get<ItemRepository>();
    }

    private readonly IItemRepository _repository;
    [HttpGet]
    public JsonResult GetItems()
    {
        return new JsonResult(_repository.GetItems());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetItemInfo(int id)
    {
        var result = _repository.GetItemInfo(id);
        return result.IsValid ? new JsonResult(result) : BadRequestResult;
    }

    [HttpGet("{id:int}/img")]
    public IActionResult GetItemImage(int id)
    {
        var (pic, ext) = _repository.GetItemImage(id);
        return pic.Any() ? new FileStreamResult(new MemoryStream(pic), $"image/{ext}") : BadRequestResult;
    }
}