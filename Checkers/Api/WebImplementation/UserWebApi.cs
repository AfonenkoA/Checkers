using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.Interface.Action;
using Checkers.Data.Entity;
using static Checkers.Api.Interface.Action.UserApiAction;
using static Checkers.CommunicationProtocol;

namespace Checkers.Api.WebImplementation;

public sealed class UserWebApi : WebApiBase, IAsyncUserApi
{
    public async Task<bool> CreateUser(UserCreationData user)
    {
        using var response = await Client.PostAsJsonAsync(UserRoute, user);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUser(Credential credential)
    {
        var route = UserRoute + Query(credential);
        using var response = await Client.DeleteAsync(route);
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, PublicUserData)> TryGetUser(int userId)
    {
        var route = UserRoute + $"/{userId}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<PublicUserData>(response);
        return res != null ? (res.IsValid, res) : (false, PublicUserData.Invalid);

    }

    public async Task<(bool, User)> TryGetSelf(Credential credential)
    {
        var route = UserRoute + Query(credential);
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<User>(response);
        return res != null ? (res.IsValid, res) : (false, User.Invalid);
    }

    //invalid
    public async Task<(bool, FriendUserData)> TryGetFriend(Credential credential, int friendId)
    {
        var route = UserRoute + $"/{friendId}" + Query(credential);
        var response = await
                Client.GetStringAsync(route);
        var res = Deserialize<FriendUserData>(response);
        return res != null ? (true, res) : (false, FriendUserData.Invalid);
    }


    public async Task<bool> SelectAnimation(Credential credential, int animationId)
    {
        var route = $"{UserRoute}/{Query(credential, UserApiAction.SelectAnimation)}";
        using var response = await Client.PutAsJsonAsync(route, animationId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SelectCheckers(Credential credential, int checkersId)
    {
        var route = UserRoute + Query(credential, UserApiAction.SelectCheckers);
        using var response = await Client.PutAsJsonAsync(route, checkersId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Authenticate(Credential user)
    {
        var route = UserRoute + Query(user, UserApiAction.Authenticate);
        using var response = await Client.PostAsJsonAsync(route,string.Empty);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserNick(Credential credential, string nick)
    {
        var route = UserRoute + Query(credential, UpdateNick);
        using var response = await Client.PutAsJsonAsync(route, nick);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserLogin(Credential credential, string login)
    {
        var route = UserRoute + Query(credential, UpdateLogin);
        using var response = await Client.PutAsJsonAsync(route, login);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserPassword(Credential credential, string password)
    {
        var route = UserRoute + Query(credential, UpdatePassword);
        using var response = await Client.PutAsJsonAsync(route, password);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserEmail(Credential credential, string email)
    {
        var route = UserRoute + Query(credential, UpdateEmail);
        using var response = await Client.PutAsJsonAsync(route, email);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserPicture(Credential credential, int pictureId)
    {
        var route = UserRoute + Query(credential, UserApiAction.UpdateUserPicture);
        using var response = await Client.PutAsJsonAsync(route, pictureId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern)
    {
        using var resp = await Client.PutAsJsonAsync($"{UserRoute}/{QueryAction(GetUsersByNick)}", pattern);
        var str = await resp.Content.ReadAsStringAsync();
        var res = Deserialize<IEnumerable<PublicUserData>>(str);
        return res != null ? (true, res) : (false, Enumerable.Empty<PublicUserData>());
    }


    public async Task<bool> AddFriend(Credential credential, int userId)
    {
        var route = UserRoute + Query(credential, UserApiAction.AddFriend);
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteFriend(Credential credential, int userId)
    {
        var route = UserRoute + Query(credential, UserApiAction.DeleteFriend);
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AcceptFriend(Credential credential, int userId)
    {
        var route = UserRoute + Query(credential, UserApiAction.AcceptFriend);
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    private const string ShopRoute = UserRoute + "/shop";
    private const string AnimationShop = ShopRoute + "/animation";
    private const string CheckersSkinShop = ShopRoute + "/checkers-skin";
    private const string LootBoxShop = ShopRoute + "/lootbox";

    public async Task<(bool, IEnumerable<int>)> TryGetAvailableAnimations(Credential c)
    {
        var response = await Client.GetStringAsync(AnimationShop+Query(c));
        var res = Deserialize<List<int>>(response);
        return res!=null ? (true,res) : (false, Enumerable.Empty<int>());
    }

    public async Task<(bool, IEnumerable<int>)> TryGetAvailableCheckers(Credential c)
    {
        var response = await Client.GetStringAsync(CheckersSkinShop + Query(c));
        var res = Deserialize<List<int>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<int>());
    }

    public async Task<(bool, IEnumerable<int>)> TryGetAvailableLootBoxes(Credential c)
    {
        var response = await Client.GetStringAsync(LootBoxShop + Query(c));
        var res = Deserialize<List<int>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<int>());
    }

    public async Task<bool> BuyCheckersSkin(Credential credential, int id)
    {
        var route = UserRoute + Query(credential, UserApiAction.BuyCheckersSkin);
        using var response = await Client.PutAsJsonAsync(route,id.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyAnimation(Credential credential, int id)
    {
        var route = UserRoute + Query(credential, UserApiAction.BuyAnimation);
        using var response = await Client.PutAsJsonAsync(route,id.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyLootBox(Credential credential, int id)
    {
        var route = UserRoute + Query(credential, UserApiAction.BuyLootBox);
        using var response = await Client.PutAsJsonAsync(route,id.ToString());
        return response.IsSuccessStatusCode;
    }
}