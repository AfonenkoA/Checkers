using Microsoft.AspNetCore.Mvc;
using WebService.Repository.Interface;
using WebService.Repository.MSSqlImplementation;
using static ApiContract.Route;

namespace WebService.Controllers;

[Route("api/" + ItemRoute)]
[ApiController]
public class ItemController : ControllerBase
{

    public ItemController(RepositoryFactory factory) => _repository = factory.Get<ItemRepository>();

    private readonly IItemRepository _repository;

    [HttpGet, Route("achievement")]
    public IActionResult GetAchievements() => Json(_repository.GetAchievements());

    [HttpGet, Route("animation")]
    public IActionResult GetAnimations() => Json(_repository.GetAnimations());

    [HttpGet, Route("checkers-skin")]
    public IActionResult GetCheckerSkins() => Json(_repository.GetCheckerSkins());
    [HttpGet, Route("lootbox")]
    public IActionResult GetLootBoxes() => Json(_repository.GetLootBoxes());

    [HttpGet, Route("picture")]
    public IActionResult GetPictures() => Json(_repository.GetPictures());


    [HttpGet, Route("animation/{id:int}")]
    public IActionResult GetAnimation(int id) => Json(_repository.GetAnimation(id));

    [HttpGet, Route("checkers-skin/{id:int}")]
    public IActionResult GetCheckersSkin(int id) => Json(_repository.GetCheckersSkin(id));

    [HttpGet, Route("lootbox/{id:int}")]
    public IActionResult GetLootBox(int id) => Json(_repository.GetLootBox(id));

    [HttpGet, Route("picture/{id:int}")]
    public IActionResult GetPicture(int id) => Json(_repository.GetPicture(id));

    [HttpGet, Route("achievement/{id:int}")]
    public IActionResult GetAchievement(int id) => Json(_repository.GetAchievement(id));
}