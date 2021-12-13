using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IStatisticsRepository
{
    IDictionary<int, BasicUserData> GetTopPlayers();
    IDictionary<int, BasicUserData> GetTopPlayers(Credential credential);
}