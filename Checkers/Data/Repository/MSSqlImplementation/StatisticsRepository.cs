using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class StatisticsRepository : Repository, IStatisticsRepository
{

    public const string SelectTopPlayersProc = "[SP_SelectTopPlayers]";
    public const string SelectTopPlayersAuthProc = "[SP_SelectTopPlayersAuth]";

    public const string StatisticPosition = "RowNumber";
    public const string OrderedPlayers = "OrderedPlayers";

    internal StatisticsRepository(SqlConnection connection) : base(connection) { }

    public IDictionary<int, PublicUserData> GetTopPlayers()
    {
        using var command = CreateProcedure(SelectTopPlayersProc);
        using var reader = command.ExecuteReader();
        var dict = new Dictionary<int,PublicUserData>();
        while (reader.Read())
            dict.Add(reader.GetFieldValue<int>(StatisticPosition),reader.GetUser());
        return dict;
    }

    public IDictionary<int, PublicUserData> GetTopPlayers(Credential credential)
    {
        using var command = CreateProcedure(SelectTopPlayersProc);
        command.Parameters.AddRange(
            new []
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password)
            });
        using var reader = command.ExecuteReader();
        var dict = new Dictionary<int, PublicUserData>();
        while (reader.Read())
            dict.Add(reader.GetFieldValue<int>(StatisticPosition), reader.GetUser());
        return dict;
    }
}