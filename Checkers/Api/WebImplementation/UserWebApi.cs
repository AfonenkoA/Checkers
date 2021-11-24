using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data;
using Checkers.Data.Entity;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Api.WebImplementation;

public sealed class UserWebApi : WebApiBase, IAsyncUserApi
{
    public Task<bool> CreateUser(UserCreationData user) =>
        Client.PostAsJsonAsync(UserRoute, user)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> DeleteUser(Credential credential) =>
        Client.DeleteAsync(UserRoute + Query(credential))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<(bool, PublicUserData)> TryGetUser(int userId) =>
        Client.GetStringAsync(UserRoute + $"/{userId}")
            .ContinueWith(task => Deserialize<PublicUserData>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, PublicUserData.Invalid);
            });

    public Task<(bool, User)> TryGetSelf(Credential credential) =>
        Client.GetStringAsync(UserRoute + Query(credential))
            .ContinueWith(task => Deserialize<User>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, User.Invalid);
            });

    public Task<(bool, FriendUserData)> TryGetFriend(Credential credential, int friendId) =>
        Client.GetStringAsync(UserRoute + Query(credential, friendId))
            .ContinueWith(task => Deserialize<FriendUserData>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, FriendUserData.Invalid);
            });


    public Task<bool> SelectAnimation(Credential credential, int animationId) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.SelectAnimation, animationId))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> SelectCheckers(Credential credential, int checkersId) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.SelectCheckers, checkersId))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> BuyItem(Credential credential, int itemId) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.Buy, itemId))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> Authenticate(Credential user) =>
        Client.GetAsync(UserRoute + Query(user, UserApiAction.Authenticate))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);


    public Task<bool> UpdateUserNick(Credential credential, string nick) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.UpdateNick, nick))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateUserLogin(Credential credential, string login) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.UpdateLogin, login))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateUserPassword(Credential credential, string password) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.UpdatePassword, password))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateUserEmail(Credential credential, string email) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.UpdateEmail, email))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern) =>
        Client.GetStringAsync(UserRoute + Query(UserApiAction.GetUsersByNick, pattern))
            .ContinueWith(task => Deserialize<IEnumerable<PublicUserData>>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, Enumerable.Empty<PublicUserData>());
            });


    public Task<bool> AddFriend(Credential credential, int userId) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.AddFriend, userId))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> DeleteFriend(Credential credential, int userId) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.DeleteFriend, userId))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> AcceptFriend(Credential credential, int userId) =>
        Client.GetAsync(UserRoute + Query(credential, UserApiAction.AcceptFriend, userId))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);
}