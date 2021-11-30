using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class UserTest
{
    private const string Login = "log";
    private const string Password = "pass";
    private const string Nick = "nick";
    private const string Email = "email";

    private const string NewLogin = "new log";
    private const string NewPassword = "new pass";
    private const string NewNick = "new nick";
    private const string NewEmail = "new email";


    private static readonly IAsyncUserApi UserApi = new UserWebApi();
    private static readonly Credential Credential = new(){Login = Login,Password = Password};


    [TestMethod]
    public async Task CreateUser()
    {
        var data = new UserCreationData {Login = Login, Email = Email, Nick = Nick, Password = Password};
        var success = await UserApi.CreateUser(data);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Authenticate()
    {
        var success =  await UserApi.Authenticate(Credential);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task GetUser()
    {
        var (success,_) = await UserApi.TryGetUser(1);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task GetSelf()
    {
        var (success, _) = await UserApi.TryGetSelf(Credential);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task SelectAnimation()
    {
        var success = await UserApi.SelectAnimation(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task SelectCheckers()
    {
        var success = await UserApi.SelectCheckers(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task BuyItem()
    {
        var success = await UserApi.BuyItem(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task UpdateUserNick()
    {
        var success = await UserApi.UpdateUserNick(Credential, NewNick);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task UpdateUserLogin()
    {
        var success = await UserApi.UpdateUserLogin(Credential, NewLogin);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task UpdateUserPassword()
    {
        var success = await UserApi.UpdateUserPassword(Credential, NewPassword);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task UpdateUserEmail()
    {
        var success = await UserApi.UpdateUserEmail(Credential, NewEmail);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task UpdateUserPicture()
    {
        var success = await UserApi.UpdateUserPicture(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task TryGetUsersByNick()
    {
        var (success,users) = await UserApi.TryGetUsersByNick("i");
        Assert.IsTrue(users.Any());
        Assert.IsTrue(success);
    }
}