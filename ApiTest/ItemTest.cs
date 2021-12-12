using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class ItemTest
{
    private static readonly IAsyncItemApi ItemApi = new ItemWebApi();
    private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();

    private static readonly List<Achievement> Achievements = new();
    private static readonly List<Animation> Animations = new();
    private static readonly List<CheckersSkin> CheckersSkin = new();
    private static readonly List<Picture> Pictures = new();
    private static readonly List<LootBox> LootBoxes = new();

    [TestMethod]
    public async Task Test01GetAllAchievement()
    {
        var (success, items) = await ItemApi.TryGetAchievements();
        Assert.IsTrue(success);
        Achievements.AddRange(items);
        foreach(var achievement in Achievements)
            Assert.IsTrue(achievement.IsValid);
    }

    [TestMethod]
    public async Task Test02GetAllAnimation()
    {
        var (success, items) = await ItemApi.TryGetAnimations();
        Assert.IsTrue(success);
        Animations.AddRange(items);
        foreach (var achievement in Animations)
            Assert.IsTrue(achievement.IsValid);
    }

    [TestMethod]
    public async Task Test03GetAllCheckersSkin()
    {
        var (success, items) = await ItemApi.TryGetCheckerSkins();
        Assert.IsTrue(success);
        CheckersSkin.AddRange(items);
        foreach (var achievement in CheckersSkin)
            Assert.IsTrue(achievement.IsValid);
    }

    [TestMethod]
    public async Task Test04GetAllPicture()
    {
        var (success, items) = await ItemApi.TryGetPictures();
        Assert.IsTrue(success);
        Pictures.AddRange(items);
        foreach (var achievement in Pictures)
            Assert.IsTrue(achievement.IsValid);
    }

    [TestMethod]
    public async Task Test05GetAllLootBox()
    {
        var (success, items) = await ItemApi.TryGetLootBoxes();
        Assert.IsTrue(success);
        LootBoxes.AddRange(items);
        foreach (var achievement in LootBoxes)
            Assert.IsTrue(achievement.IsValid);
    }

    [TestMethod]
    public async Task Test06GetAchievement()
    {
        Assert.IsTrue(Achievements.Any());
        foreach (var item in Achievements)
        {
            var (success, result) = await ItemApi.TryGetAchievement(item.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(result.IsValid);
            var (fileSuccess, file) = await ResourceService.TryGetFile(result.Id);
            Assert.IsTrue(fileSuccess);
            Assert.IsTrue(file.Any());
        }
    }

    [TestMethod]
    public async Task Test07GetAnimation()
    {
        Assert.IsTrue(Animations.Any());
        foreach (var item in Animations)
        {
            var (success, result) = await ItemApi.TryGetAnimation(item.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(result.IsValid);
            var (fileSuccess, file) = await ResourceService.TryGetFile(result.Id);
            Assert.IsTrue(fileSuccess);
            Assert.IsTrue(file.Any());
        }
    }

    [TestMethod]
    public async Task Test08GetLootBox()
    {
        Assert.IsTrue(LootBoxes.Any());
        foreach (var item in LootBoxes)
        {
            var (success, result) = await ItemApi.TryGetLootBox(item.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(result.IsValid);
            var (fileSuccess, file) = await ResourceService.TryGetFile(result.Id);
            Assert.IsTrue(fileSuccess);
            Assert.IsTrue(file.Any());
        }
    }

    [TestMethod]
    public async Task Test09GetPicture()
    {
        Assert.IsTrue(Pictures.Any());
        foreach (var item in Pictures)
        {
            var (success, result) = await ItemApi.TryGetPicture(item.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(result.IsValid);
            var (fileSuccess, file) = await ResourceService.TryGetFile(result.Id);
            Assert.IsTrue(fileSuccess);
            Assert.IsTrue(file.Any());
        }
    }

    [TestMethod]
    public async Task Test10GetCheckersSkin()
    {
        Assert.IsTrue(CheckersSkin.Any());
        foreach (var item in CheckersSkin)
        {
            var (success, result) = await ItemApi.TryGetCheckersSkin(item.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(result.IsValid);
            var (fileSuccess, file) = await ResourceService.TryGetFile(result.Id);
            Assert.IsTrue(fileSuccess);
            Assert.IsTrue(file.Any());
        }
    }

    [TestMethod]
    public async Task Blya()
    {
        for (int i = 0; i < 1000; i++)
            await ResourceService.TryGetFile(2);
    }
}