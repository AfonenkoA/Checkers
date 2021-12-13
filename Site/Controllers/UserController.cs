using System;
using Checkers.Site.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Checkers.Site.Data.Interfaces;
using Checkers.Site.Data.Mocks;

namespace Checkers.Site.Controllers;
   
public class UserController : Controller
{
    private readonly IUserRep _Users;

    public UserController(IUserRep iUsers)
    {
        _Users = iUsers;
    }

    public ViewResult List()
    {
        var users = _Users;
        return View(users);
    }
}
