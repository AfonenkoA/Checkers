using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;

namespace Checkers.Data.Repository.MSSqlImplementation;

internal class StatisticsRepository : Repository, IStatisticsRepository
{
    public IDictionary<int, PublicUserData> GetTopPlayers()
    {
        throw new System.NotImplementedException();
    }

    public IDictionary<int, PublicUserData> GetTopPlayers(Credential credential)
    {
        throw new System.NotImplementedException();
    }
}