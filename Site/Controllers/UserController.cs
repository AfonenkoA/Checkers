using System;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Site.Controllers;
   
public class UserController : Controller
{
  
    private static readonly IAsyncUserApi api = new UserWebApi();
    private static readonly IAsyncResourceService resourseApi = new AsyncResourceWebApi();

    public async Task<ViewResult> List()
    {
        var (_, Users) = await api.TryGetUsersByNick("");
        return View(Users);
    }

    public async Task<ViewResult> Picture()
    {
        var (_, User) = await api.TryGetUser(1);
        var str = resourseApi.GetFileUrl(User.PictureId);
        return View((object)str);
    }
}
