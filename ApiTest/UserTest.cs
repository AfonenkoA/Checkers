using System.Linq;
using System.Threading.Tasks;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
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
    private static readonly IAsyncItemApi ItemApi = new ItemWebApi();

    private static int _newAnimation;
    private static int _newCheckersSkin;
    private static int _newPicture;


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
    public async Task Test05UpdateUserNick()
    {
        var success = await UserApi.UpdateUserNick(Credential, NewNick);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test06UpdateUserEmail()
    {
        var success = await UserApi.UpdateUserEmail(Credential, NewEmail);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test07UpdateUserPicture()
    {
        var (picSuccess, items) = await ItemApi.TryGetPictures();
        Assert.IsTrue(picSuccess);
        _newPicture = items.Select(i=>i.Id).Max();
        var success = await UserApi.UpdateUserPicture(Credential, _newPicture);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test08TryGetUsersByNick()
    {
        var (success,users) = await UserApi.TryGetUsersByNick("");
        Assert.IsTrue(users.Any());
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test09BuyAnimation()
    {
        var (shopSuccess, items) = await UserApi.TryGetAvailableAnimations(Credential);
        Assert.IsTrue(shopSuccess);
        var enumerable = items.ToList();
        Assert.IsTrue(enumerable.Any());
        _newAnimation = enumerable.Max();
        var success = await UserApi.BuyAnimation(Credential, _newAnimation);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test10BuyCheckersSkin()
    {
        var (shopSuccess, items) = await UserApi.TryGetAvailableCheckers(Credential);
        Assert.IsTrue(shopSuccess);
        var enumerable = items.ToList();
        Assert.IsTrue(enumerable.Any());
        _newCheckersSkin = enumerable.Max();
        var success = await UserApi.BuyCheckersSkin(Credential, _newCheckersSkin);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test11SelectAnimation()
    {
        var success = await UserApi.SelectAnimation(Credential, _newAnimation);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test12SelectCheckers()
    {
        var success = await UserApi.SelectCheckers(Credential, _newCheckersSkin);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test13BuyLootBox()
    {
        var (shopSuccess, items) = await UserApi.TryGetAvailableLootBoxes(Credential);
        Assert.IsTrue(shopSuccess);
        var enumerable = items.ToList();
        Assert.IsTrue(enumerable.Any());
        var success = await UserApi.BuyLootBox(Credential, enumerable.Max());
        Assert.IsTrue(success);
    }


    [TestMethod]
    public async Task Test14UpdateUserLogin()
    {
        var success = await UserApi.UpdateUserLogin(Credential, NewLogin);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test15UpdateUserPassword()
    {
        var success = await UserApi.UpdateUserPassword(new Credential {Login = NewLogin,Password = Password}, NewPassword);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test16Final()
    {
        var newCredential = new Credential {Login = NewLogin, Password = NewPassword};
        var (success, user) = await UserApi.TryGetSelf(newCredential);
        Assert.IsTrue(success);
        Assert.AreEqual(NewNick,user.Nick);
        Assert.AreEqual(_newCheckersSkin,user.SelectedCheckersId);
        Assert.AreEqual(_newAnimation, user.SelectedAnimationId);
        Assert.AreEqual(_newPicture,user.PictureId);
    }
}