using Common.Entity;

namespace Api.Interface;

public interface IAsyncStatisticsApi
{
    Task<(bool, IDictionary<long, PublicUserData>)> TryGetTopPlayers();
    Task<(bool, IDictionary<long, PublicUserData>)> TryGetTopPlayers(Credential credential);
    string GetOnlineImageUrl();
}