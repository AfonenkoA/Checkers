using System.Net.Http.Json;
using Api.Interface;
using ApiContract.Action;
using Common.Entity;
using GameModel;
using static ApiContract.Action.UserApiAction;
using static ApiContract.Route;
using static Common.CommunicationProtocol;

namespace Api.WebImplementation;

public sealed class UserWebApi : WebApiBase, IAsyncUserApi
{
    public async Task<bool> CreateUser(UserCreationData user)
    {
        using var response = await Client.PostAsJsonAsync(UserRoute, user);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUser(ICredential credential)
    {
        var route = $"{UserRoute}{Query(credential)}";
        using var response = await Client.DeleteAsync(route);
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, PublicUserData)> TryGetUser(int userId)
    {
        var route = $"{UserRoute}/{userId}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<PublicUserData>(response);
        return res != null ? (res.IsValid, res) : (false, PublicUserData.Invalid);

    }

    public async Task<(bool, User)> TryGetSelf(ICredential credential)
    {
        var route = $"{UserRoute}{Query(credential)}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<User>(response);
        return res != null ? (res.IsValid, res) : (false, User.Invalid);
    }

    //invalid
    public async Task<(bool, FriendUserData)> TryGetFriend(ICredential credential, int friendId)
    {
        var route = $"{UserRoute}/{friendId}{Query(credential)}";
        var response = await
                Client.GetStringAsync(route);
        var res = Deserialize<FriendUserData>(response);
        return res != null ? (true, res) : (false, FriendUserData.Invalid);
    }

    public Task<(bool, IEnumerable<GameInfo>)> TryGetGames(ICredential credential)
    {
        throw new NotImplementedException();
    }


    public async Task<bool> SelectAnimation(ICredential credential, int animationId)
    {
        var route = $"{UserRoute}/{Query(credential, UserApiAction.SelectAnimation)}";
        using var response = await Client.PutAsJsonAsync(route, animationId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SelectCheckers(ICredential credential, int checkersId)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.SelectCheckers)}";
        using var response = await Client.PutAsJsonAsync(route, checkersId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Authenticate(ICredential user)
    {
        var route = $"{UserRoute}{Query(user, UserApiAction.Authenticate)}";
        using var response = await Client.PutAsJsonAsync(route, string.Empty);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserNick(ICredential credential, string nick)
    {
        var route = UserRoute + Query(credential, UpdateNick);
        using var response = await Client.PutAsJsonAsync(route, nick);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserLogin(ICredential credential, string login)
    {
        var route = $"{UserRoute}{Query(credential, UpdateLogin)}";
        using var response = await Client.PutAsJsonAsync(route, login);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserPassword(ICredential credential, string password)
    {
        var route = $"{UserRoute}{Query(credential, UpdatePassword)}";
        using var response = await Client.PutAsJsonAsync(route, password);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserEmail(ICredential credential, string email)
    {
        var route = $"{UserRoute}{Query(credential, UpdateEmail)}";
        using var response = await Client.PutAsJsonAsync(route, email);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserPicture(ICredential credential, int pictureId)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.UpdateUserPicture)}";
        using var response = await Client.PutAsJsonAsync(route, pictureId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern)
    {
        var route = $"{UserRoute}/{QueryAction(GetUsersByNick)}";
        using var resp = await Client.PutAsJsonAsync(route, pattern);
        var str = await resp.Content.ReadAsStringAsync();
        var res = Deserialize<IEnumerable<PublicUserData>>(str);
        return res != null ? (true, res) : (false, Enumerable.Empty<PublicUserData>());
    }


    public async Task<bool> AddFriend(ICredential credential, int userId)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.AddFriend)}";
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteFriend(ICredential credential, int userId)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.DeleteFriend)}";
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AcceptFriend(ICredential credential, int userId)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.AcceptFriend)}";
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyCheckersSkin(ICredential credential, int id)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.BuyCheckersSkin)}";
        using var response = await Client.PutAsJsonAsync(route, id.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyAnimation(ICredential credential, int id)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.BuyAnimation)}";
        using var response = await Client.PutAsJsonAsync(route, id.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyLootBox(ICredential credential, int id)
    {
        var route = $"{UserRoute}{Query(credential, UserApiAction.BuyLootBox)}";
        using var response = await Client.PutAsJsonAsync(route, id.ToString());
        return response.IsSuccessStatusCode;
    }
}