using System.Collections.Generic;
using System.Data.SqlClient;
using Common.Entity;
using WebService.Repository.Interface;
using static WebService.Repository.MSSqlImplementation.SqlExtensions;

namespace WebService.Repository.MSSqlImplementation;

public sealed class StatisticsRepository : UserRepositoryBase, IStatisticsRepository
{

    public const string SelectTopPlayersProc = "[SP_SelectTopPlayers]";
    public const string SelectTopPlayersAuthProc = "[SP_SelectTopPlayersAuth]";

    public const string StatisticPosition = "RowNumber";
    public const string OrderedPlayers = "OrderedPlayers";
    public const string ShowCount = "10";

    internal StatisticsRepository(SqlConnection connection) : base(connection) { }


    private IDictionary<long, int> GetTopPlayersId()
    {
        using var command = CreateProcedure(SelectTopPlayersProc);
        using var reader = command.ExecuteReader();
        var dict = reader.GetTopUsers();
        return dict;
    }

    private IDictionary<long, int> GetTopPlayersIdAuth(Credential credential)
    {
        using var command = CreateProcedure(SelectTopPlayersAuthProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password)
            });
        using var reader = command.ExecuteReader();
        var dict = reader.GetTopUsers();
        return dict;
    }

    public IDictionary<long, PublicUserData> GetTopPlayers()
    {
        var idDict = GetTopPlayersId();
        var dict = new Dictionary<long, PublicUserData>();
        foreach (var (key,value) in idDict)
            dict.Add(key,GetPublicUserDataUser(value));
        return dict;
    }

    public IDictionary<long, PublicUserData> GetTopPlayers(Credential credential)
    {
        var idDict = GetTopPlayersIdAuth(credential);
        var dict = new Dictionary<long, PublicUserData>();
        foreach (var (key, value) in idDict)
            dict.Add(key, GetPublicUserDataUser(value));
        return dict;
    }
}