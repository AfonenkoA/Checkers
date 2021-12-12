using Api.Interface;
using Common.Entity;
using static Common.CommunicationProtocol;
using static ApiContract.Route;

namespace Api.WebImplementation;

public sealed class ItemWebApi : WebApiBase, IAsyncItemApi
{

    public async Task<(bool, IEnumerable<Achievement>)> TryGetAchievements()
    {
        var response = await Client.GetStringAsync(AchievementRoute);
        var res = Deserialize<IEnumerable<Achievement>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<Achievement>());
    }

    public async Task<(bool, IEnumerable<Animation>)> TryGetAnimations()
    {
        var response = await Client.GetStringAsync(AnimationRoute);
        var res = Deserialize<IEnumerable<Animation>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<Animation>());
    }

    public async Task<(bool, IEnumerable<CheckersSkin>)> TryGetCheckerSkins()
    {
        var response = await Client.GetStringAsync(CheckersSkinRoute);
        var res = Deserialize<IEnumerable<CheckersSkin>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<CheckersSkin>());
    }

    public async Task<(bool, IEnumerable<LootBox>)> TryGetLootBoxes()
    {
        var response = await Client.GetStringAsync(LootBoxRoute);
        var res = Deserialize<IEnumerable<LootBox>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<LootBox>());
    }

    public async Task<(bool, IEnumerable<Picture>)> TryGetPictures()
    {
        var response = await Client.GetStringAsync(PictureRoute);
        var res = Deserialize<IEnumerable<Picture>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<Picture>());
    }

    public async Task<(bool, Achievement)> TryGetAchievement(int id)
    {
        var response = await Client.GetStringAsync($"{AchievementRoute}/{id}");
        var res = Deserialize<Achievement>(response);
        return res != null ? (res.IsValid, res) : (false, Achievement.Invalid);
    }

    public async Task<(bool, Animation)> TryGetAnimation(int id)
    {
        var response = await Client.GetStringAsync($"{AnimationRoute}/{id}");
        var res = Deserialize<Animation>(response);
        return res != null ? (res.IsValid, res) : (false, Animation.Invalid);
    }

    public async Task<(bool, CheckersSkin)> TryGetCheckersSkin(int id)
    {
        var response = await Client.GetStringAsync($"{CheckersSkinRoute}/{id}");
        var res = Deserialize<CheckersSkin>(response);
        return res != null ? (res.IsValid, res) : (false, CheckersSkin.Invalid);
    }

    public async Task<(bool, LootBox)> TryGetLootBox(int id)
    {
        var response = await Client.GetStringAsync($"{LootBoxRoute}/{id}");
        var res = Deserialize<LootBox>(response);
        return res != null ? (res.IsValid, res) : (false, LootBox.Invalid);
    }

    public async Task<(bool, Picture)> TryGetPicture(int id)
    {
        var response = await Client.GetStringAsync($"{PictureRoute}/{id}");
        var res = Deserialize<Picture>(response);
        return res != null ? (res.IsValid, res) : (false, Picture.Invalid);
    }
}