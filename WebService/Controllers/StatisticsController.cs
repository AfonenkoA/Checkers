using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using WebService.Repository.Interface;
using WebService.Repository.MSSqlImplementation;
using static ApiContract.Route;

namespace WebService.Controllers;

[ApiController]
[Route("api/" + StatisticsRoute)]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsRepository _repository;

    public StatisticsController(RepositoryFactory factory) 
        => _repository = factory.Get<StatisticsRepository>();

    [HttpGet]
    public IActionResult GetTopPlayers([FromQuery] Credential credential) =>
        Json(credential.IsValid ? _repository.GetTopPlayers(credential) :
            _repository.GetTopPlayers());
}