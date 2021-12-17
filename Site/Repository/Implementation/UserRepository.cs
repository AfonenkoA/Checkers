using Api.Interface;
using Common.Entity;
using Site.Data.Models.User;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class UserRepository : IUserRepository
{
    private readonly IAsyncUserApi _userApi;
    private readonly IItemRepository _item;
    private readonly IAsyncStatisticsApi _statisticsApi;

    public UserRepository(IAsyncUserApi userApi,
        IAsyncStatisticsApi statisticsApi, IItemRepository item)
    {
        _userApi = userApi;
        _statisticsApi = statisticsApi;
        _item = item;
    }

    private async Task<UserInfo> ConvertToUserInfo(PublicUserData u) =>
        new(u, await _item.GetPicture(u.Picture.Id));

    public async Task<(bool, UserInfo)> GetUser(int id)
    {
        var (success, user) = await _userApi.TryGetUser(id);
        return (success, await ConvertToUserInfo(user));
    }

    public async Task<(bool, IEnumerable<UserInfo>)> GetByNick(string pattern)
    {
        var (success, data) = await _userApi.TryGetUsersByNick(pattern);
        var users = new List<UserInfo>();
        foreach (var p in data)
            users.Add(await ConvertToUserInfo(p));
        return (success, users);
    }

    public Task<bool> Authorize(ICredential credential) => _userApi.Authenticate(credential);

    public async Task<(bool, Self)> GetSelf(ICredential credential)
    {
        var (success, user) = await _userApi.TryGetSelf(credential);
        var friends = new List<Friend>();
        foreach (var friend in user.Friends)
        {
            var (_, f) = await _userApi.TryGetFriend(credential, friend.Id);

            friends.Add(new Friend(f, await _item.GetPicture(f.Picture.Id)));
        }

        return (success, new Self(await ConvertToUserInfo(user),
            await _item.GetPicture(user.Picture.Id),
            friends,
            await _item.GetPictures()));
    }

    public async Task<(bool, IDictionary<long, UserInfo>)> GetTop()
    {
        var (success, users) = await _statisticsApi.TryGetTopPlayers();
        var res = new Dictionary<long, UserInfo>();
        foreach (var (pos, user) in users)
            res.Add(pos, await ConvertToUserInfo(user));
        return (success, res);
    }

    public async Task<(bool, IDictionary<long, UserInfo>)> GetTop(ICredential credential)
    {
        var (success, users) = await _statisticsApi.TryGetTopPlayers(credential);
        var res = new Dictionary<long, UserInfo>();
        foreach (var (pos, user) in users)
            res.Add(pos, await ConvertToUserInfo(user));
        return (success, res);
    }

    public Task<bool> UpdateUserNick(ICredential credential, string nick) =>
        _userApi.UpdateUserNick(credential, nick);

    public Task<bool> UpdateUserLogin(ICredential credential, string login) =>
        _userApi.UpdateUserLogin(credential, login);

    public Task<bool> UpdateUserPassword(ICredential credential, string password) =>
        _userApi.UpdateUserPassword(credential, password);

    public Task<bool> UpdateUserEmail(ICredential credential, string email) =>
        _userApi.UpdateUserEmail(credential, email);

    public Task<bool> UpdateUserPicture(ICredential credential, int id) =>
        _userApi.UpdateUserPicture(credential, id);
}