using Api.Interface;
using Common.Entity;

namespace Api.WebImplementation;

public sealed class StatisticsWebApi : WebApiBase, IAsyncStatisticsApi
{
    public Task<(bool, IDictionary<int, PublicUserData>)> TryGetTopPlayers()
    {
        throw new System.NotImplementedException();
    }

    public Task<(bool, IDictionary<int, PublicUserData>)> TryGetTopPlayers(Credential credential)
    {
        throw new System.NotImplementedException();
    }

    public string GetOnlineImageUrl()
    {
        throw new System.NotImplementedException();
    }
}