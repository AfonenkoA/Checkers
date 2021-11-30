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
    public async Task Test01CreateUser()
    {
        var data = new UserCreationData {Login = Login, Email = Email, Nick = Nick, Password = Password};
        var success = await UserApi.CreateUser(data);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test02Authenticate()
    {
        var success =  await UserApi.Authenticate(Credential);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test03GetUser()
    {
        var (success,_) = await UserApi.TryGetUser(1);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test04GetSelf()
    {
        var (success, _) = await UserApi.TryGetSelf(Credential);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test05SelectAnimation()
    {
        var success = await UserApi.SelectAnimation(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test06SelectCheckers()
    {
        var success = await UserApi.SelectCheckers(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test07UpdateUserNick()
    {
        var success = await UserApi.UpdateUserNick(Credential, NewNick);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test08UpdateUserEmail()
    {
        var success = await UserApi.UpdateUserEmail(Credential, NewEmail);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test09UpdateUserPicture()
    {
        var success = await UserApi.UpdateUserPicture(Credential, 2);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test10TryGetUsersByNick()
    {
        var (success,users) = await UserApi.TryGetUsersByNick("i");
        Assert.IsTrue(users.Any());
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test11UpdateUserLogin()
    {
        var success = await UserApi.UpdateUserLogin(Credential, NewLogin);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test12UpdateUserPassword()
    {
        var success = await UserApi.UpdateUserPassword(new Credential {Login = NewLogin,Password = Password}, NewPassword);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test13Final()
    {
        var newCredential = new Credential {Login = NewLogin, Password = NewPassword};
        var (success, user) = await UserApi.TryGetSelf(newCredential);
        Assert.IsTrue(success);
        Assert.AreEqual(NewNick,user.Nick);
        Assert.AreEqual(2,user.SelectedCheckersId);
        Assert.AreEqual(2, user.SelectedAnimationId);
        Assert.AreEqual(2,user.PictureId);
    }
}