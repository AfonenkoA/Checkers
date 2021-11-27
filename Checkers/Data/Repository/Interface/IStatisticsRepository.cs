using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

public interface IStatisticsRepository
{
    IDictionary<int, BasicUserData> GetTopPlayers();
    IDictionary<int, BasicUserData> GetTopPlayers(Credential credential);
}