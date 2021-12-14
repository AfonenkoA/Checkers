using System;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Data.Models;

namespace Checkers.Site.Controllers;
   
public class UserController : Controller
{
  
    private static readonly IAsyncUserApi api = new UserWebApi();
    private static readonly IAsyncResourceService resourseApi = new AsyncResourceWebApi();

    public async Task<ViewResult> Get(int id)
    {
        var (_, User) = await api.TryGetUser(id);
        var picture = resourseApi.GetFileUrl(User.Picture.Resource.Id);
        var model = new PublicUserModel(User) { PictureUrl = picture };
        return View(model);
    }
    public async Task<ViewResult> List()
    {
        var (_, Users) = await api.TryGetUsersByNick("");
        return View(Users);
    }

    public async Task<ViewResult> Nick()
    {
        var (_, User) = await api.TryGetUser(1);
        var str = User.Nick;
        return View((object)str); ;
    }

    public async Task<ViewResult> LastActivity()
    {
        var (_, User) = await api.TryGetUser(1);
        var str = User.LastActivity;
        return View(str);
    }

    public async Task<ViewResult> Picture()
    {
        var (_, User) = await api.TryGetUser(1);
        var str = resourseApi.GetFileUrl(User.Picture.Resource.Id);
        return View((object)str);
    }

    public async Task<ViewResult> DataUser()
    {
        var (_, User) = await api.TryGetUser(1);
        var str = resourseApi.GetFileUrl(User.Picture.Resource.Id);
        var activity = User.LastActivity;
        var nick = User.Nick;
        var rating = User.SocialCredit;
        return View((object)str);
    }

}
