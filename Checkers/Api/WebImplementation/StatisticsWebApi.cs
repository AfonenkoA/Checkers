using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public class StatisticsWebApi : WebApiBase, IAsyncStatisticsApi
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