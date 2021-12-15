using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Api.Interface;
using Api.WebImplementation;
using Site.Data.Models;

namespace Site.Controllers
{
    public sealed class StatisticsController : Controller
    {
        private static readonly IAsyncStatisticsApi Api = new StatisticsWebApi();
        private static readonly IAsyncResourceService ResourceApi = new AsyncResourceWebApi();
        public async Task<ViewResult> Get(Credential credential)
        {
            IDictionary<long, PublicUserData> data;
            bool success;
            if (credential.IsValid)
            {
                (success, data) = await Api.TryGetTopPlayers(credential);
                if (!success) return View("Error");
            }
            else
            {
                (success, data) = await Api.TryGetTopPlayers();
                if (!success) return View("Error");
            }
            IDictionary<long, PublicUserModel> res = new Dictionary<long, PublicUserModel>();
            foreach (var (key, value) in data)
                res.Add(key, new PublicUserModel(new Credential(),value)
                {
                    PictureUrl = ResourceApi.GetFileUrl(value.Picture.Resource.Id)
                });
            return View(res);
        }
    }
}
