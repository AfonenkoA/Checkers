using Api.Interface;
using Common.Entity;
using Site.Data.Models.User;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class UserRepository : IUserRepository
{
    private readonly IAsyncUserApi _userApi;
    private readonly IAsyncResourceService _resourceService;
    private readonly IAsyncStatisticsApi _statisticsApi;

    public UserRepository(IAsyncUserApi userApi, IAsyncResourceService resourceService, IAsyncStatisticsApi statisticsApi)
    {
        _userApi = userApi;
        _resourceService = resourceService;
        _statisticsApi = statisticsApi;
    }

    private string GetPictureUrl(PublicUserData data) =>
        _resourceService.GetFileUrl(data.Picture.Resource.Id);

    private UserInfo ConvertToUserInfo(PublicUserData u) =>
        new(u, GetPictureUrl(u));

    public async Task<(bool, UserInfo)> GetUser(int id)
    {
        var (success, user) = await _userApi.TryGetUser(id);
        return (success, ConvertToUserInfo(user));
    }

    public async Task<(bool, IEnumerable<UserInfo>)> GetByNick(string pattern)
    {
        var (success, users) = await _userApi.TryGetUsersByNick(pattern);
        return (success, users.Select(ConvertToUserInfo));
    }

    public Task<bool> Authorize(Credential credential) => _userApi.Authenticate(credential);

    public async Task<(bool, Self)> GetSelf(Credential credential)
    {
        var (success, user) = await _userApi.TryGetSelf(credential);
        var friends = new List<Friend>();
        foreach (var friend in user.Friends)
        {
            var (s, f) =  await _userApi.TryGetFriend(credential,friend.Id);
            if (!s) return (s,new Self(ConvertToUserInfo(user)));
            friends.Add(new Friend(f,GetPictureUrl(f)));   
        }
        return (success, new Self(ConvertToUserInfo(user)){Friends = friends});
    }

    public async Task<(bool, IDictionary<long, UserInfo>)> GetTop()
    {
        var (success, users) = await _statisticsApi.TryGetTopPlayers();
        var res = new Dictionary<long, UserInfo>();
        foreach (var (pos, user) in users)
            res.Add(pos,ConvertToUserInfo(user));
        return (success,res);
    }

    public async Task<(bool, IDictionary<long, UserInfo>)> GetTop(Credential credential)
    {
        var (success, users) = await _statisticsApi.TryGetTopPlayers(credential);
        var res = new Dictionary<long, UserInfo>();
        foreach (var (pos, user) in users)
            res.Add(pos, ConvertToUserInfo(user));
        return (success, res);
    }
}