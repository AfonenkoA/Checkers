using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncStatisticsApi
{
    Task<(bool, IDictionary<int,PublicUserData>)> TryGetTopPlayers();
    Task<(bool, IDictionary<int, PublicUserData>)> TryGetTopPlayers(Credential credential);
    string GetOnlineImageUrl();
}