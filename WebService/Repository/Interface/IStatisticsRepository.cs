using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

public interface IStatisticsRepository
{
    IDictionary<int, BasicUserData> GetTopPlayers();
    IDictionary<int, BasicUserData> GetTopPlayers(Credential credential);
}