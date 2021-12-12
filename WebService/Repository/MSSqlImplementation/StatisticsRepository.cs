using System.Collections.Generic;
using System.Data.SqlClient;
using Common.Entity;
using WebService.Repository.Interface;
using static WebService.Repository.MSSqlImplementation.SqlExtensions;

namespace WebService.Repository.MSSqlImplementation;

public sealed class StatisticsRepository : RepositoryBase, IStatisticsRepository
{

    public const string SelectTopPlayersProc = "[SP_SelectTopPlayers]";
    public const string SelectTopPlayersAuthProc = "[SP_SelectTopPlayersAuth]";

    public const string StatisticPosition = "RowNumber";
    public const string OrderedPlayers = "OrderedPlayers";

    internal StatisticsRepository(SqlConnection connection) : base(connection) { }

    public IDictionary<int, BasicUserData> GetTopPlayers()
    {
        using var command = CreateProcedure(SelectTopPlayersProc);
        using var reader = command.ExecuteReader();
        var dict = new Dictionary<int,BasicUserData>();
        while (reader.Read())
            dict.Add(reader.GetFieldValue<int>(StatisticPosition), reader.GetUser());

        return dict;
    }



    public IDictionary<int, BasicUserData> GetTopPlayers(Credential credential)
    {
        using var command = CreateProcedure(SelectTopPlayersProc);
        command.Parameters.AddRange(
            new []
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password)
            });
        using var reader = command.ExecuteReader();
        var dict = new Dictionary<int, BasicUserData>();
        while (reader.Read())
            dict.Add(reader.GetFieldValue<int>(StatisticPosition), reader.GetUser());
        return dict;
    }
}