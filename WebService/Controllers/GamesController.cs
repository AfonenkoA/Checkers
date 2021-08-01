using Microsoft.AspNetCore.Mvc;

namespace WevService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet("{id}")]
        public string GameGet([FromRoute] int id)
        {
            return null;
        }
    }
}
