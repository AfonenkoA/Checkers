using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IStatisticsRepository
{
    IDictionary<int, PublicUserData> GetTopPlayers();
    IDictionary<int, PublicUserData> GetTopPlayers(Credential credential);
}