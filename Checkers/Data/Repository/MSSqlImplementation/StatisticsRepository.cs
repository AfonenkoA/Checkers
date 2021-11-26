using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class StatisticsRepository : Repository, IStatisticsRepository
{

    public const string SelectTopPlayersProc = "[SP_SelectTopPlayers]";
    public const string SelectTopPlayersAuthProc = "[SP_SelectTopPlayersAuth]";

    public const string StatisticPosition = "RowNumber";
    public const string OrderedPlayers = "OrderedPlayers";

    public StatisticsRepository(SqlConnection connection) : base(connection) { }

    public IDictionary<int, PublicUserData> GetTopPlayers()
    {
        throw new System.NotImplementedException();
    }

    public IDictionary<int, PublicUserData> GetTopPlayers(Credential credential)
    {
        throw new System.NotImplementedException();
    }
}