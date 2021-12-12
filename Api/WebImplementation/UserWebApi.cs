using System.Net.Http.Json;
using Api.Interface;
using ApiContract;
using ApiContract.Action;
using Common.Entity;
using static ApiContract.Action.UserApiAction;
using static Common.CommunicationProtocol;

namespace Api.WebImplementation;

public sealed class UserWebApi : WebApiBase, IAsyncUserApi
{
    public async Task<bool> CreateUser(UserCreationData user)
    {
        using var response = await Client.PostAsJsonAsync(Route.UserRoute, user);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUser(Credential credential)
    {
        var route = Route.UserRoute + Query(credential);
        using var response = await Client.DeleteAsync(route);
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, PublicUserData)> TryGetUser(int userId)
    {
        var route = Route.UserRoute + $"/{userId}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<PublicUserData>(response);
        return res != null ? (res.IsValid, res) : (false, PublicUserData.Invalid);

    }

    public async Task<(bool, User)> TryGetSelf(Credential credential)
    {
        var route = Route.UserRoute + Query(credential);
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<User>(response);
        return res != null ? (res.IsValid, res) : (false, User.Invalid);
    }

    //invalid
    public async Task<(bool, FriendUserData)> TryGetFriend(Credential credential, int friendId)
    {
        var route = Route.UserRoute + $"/{friendId}" + Query(credential);
        var response = await
                Client.GetStringAsync(route);
        var res = Deserialize<FriendUserData>(response);
        return res != null ? (true, res) : (false, FriendUserData.Invalid);
    }


    public async Task<bool> SelectAnimation(Credential credential, int animationId)
    {
        var route = $"{Route.UserRoute}/{Query(credential, UserApiAction.SelectAnimation)}";
        using var response = await Client.PutAsJsonAsync(route, animationId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SelectCheckers(Credential credential, int checkersId)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.SelectCheckers);
        using var response = await Client.PutAsJsonAsync(route, checkersId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Authenticate(Credential user)
    {
        var route = Route.UserRoute + Query(user, UserApiAction.Authenticate);
        using var response = await Client.PutAsJsonAsync(route,string.Empty);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserNick(Credential credential, string nick)
    {
        var route = Route.UserRoute + Query(credential, UpdateNick);
        using var response = await Client.PutAsJsonAsync(route, nick);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserLogin(Credential credential, string login)
    {
        var route = Route.UserRoute + Query(credential, UpdateLogin);
        using var response = await Client.PutAsJsonAsync(route, login);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserPassword(Credential credential, string password)
    {
        var route = Route.UserRoute + Query(credential, UpdatePassword);
        using var response = await Client.PutAsJsonAsync(route, password);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserEmail(Credential credential, string email)
    {
        var route = Route.UserRoute + Query(credential, UpdateEmail);
        using var response = await Client.PutAsJsonAsync(route, email);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserPicture(Credential credential, int pictureId)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.UpdateUserPicture);
        using var response = await Client.PutAsJsonAsync(route, pictureId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern)
    {
        using var resp = await Client.PutAsJsonAsync($"{Route.UserRoute}/{QueryAction(GetUsersByNick)}", pattern);
        var str = await resp.Content.ReadAsStringAsync();
        var res = Deserialize<IEnumerable<PublicUserData>>(str);
        return res != null ? (true, res) : (false, Enumerable.Empty<PublicUserData>());
    }


    public async Task<bool> AddFriend(Credential credential, int userId)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.AddFriend);
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteFriend(Credential credential, int userId)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.DeleteFriend);
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AcceptFriend(Credential credential, int userId)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.AcceptFriend);
        using var response = await Client.PutAsJsonAsync(route, userId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyCheckersSkin(Credential credential, int id)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.BuyCheckersSkin);
        using var response = await Client.PutAsJsonAsync(route,id.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyAnimation(Credential credential, int id)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.BuyAnimation);
        using var response = await Client.PutAsJsonAsync(route,id.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> BuyLootBox(Credential credential, int id)
    {
        var route = Route.UserRoute + Query(credential, UserApiAction.BuyLootBox);
        using var response = await Client.PutAsJsonAsync(route,id.ToString());
        return response.IsSuccessStatusCode;
    }
}