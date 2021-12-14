using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Api.Interface;
using Api.WebImplementation;
using Site.Data.Models;

namespace Site.Controllers
{
    public class StatisticsController : Controller
    {
        private static readonly IAsyncStatisticsApi Api = new StatisticsWebApi();
        private static readonly IAsyncResourceService resourseApi = new AsyncResourceWebApi();
        public async Task<ViewResult> Get(Credential credential)
        {
            IDictionary<long, PublicUserData> data;
            if(credential.IsValid)
               (_,data) = await Api.TryGetTopPlayers(credential);
            (_, data) = await Api.TryGetTopPlayers();

            IDictionary<long, PublicUserModel> res = new Dictionary<long, PublicUserModel>();
            foreach (var (key, value) in data)
                res.Add(key, new PublicUserModel(value)
                {
                    PictureUrl = resourseApi.GetFileUrl(value.Picture.Resource.Id)
                });
            return View(res);
        }
    }
}
