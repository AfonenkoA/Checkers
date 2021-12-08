using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
   
    public class Home : Controller
    {
        public async Task<IActionResult> Index()
        {
            var api = new UserWebApi();
            var (_,u) = await api.TryGetUsersByNick("");
            
            return View("~/Views/User/User.cshtml",u);
        }
    }
}
