using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

internal interface IStatisticsRepository
{
    IDictionary<int, PublicUserData> GetTopPlayers();
    IDictionary<int, PublicUserData> GetTopPlayers(Credential credential);
}