using Common.Entity;

namespace Api.Interface;

public interface IAsyncStatisticsApi
{
    Task<(bool, IDictionary<int,PublicUserData>)> TryGetTopPlayers();
    Task<(bool, IDictionary<int, PublicUserData>)> TryGetTopPlayers(Credential credential);
    string GetOnlineImageUrl();
}