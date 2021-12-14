using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IStatisticsRepository
{
    IDictionary<long, PublicUserData> GetTopPlayers();
    IDictionary<long, PublicUserData> GetTopPlayers(Credential credential);
}