using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace ApiTest;

[TestClass]
public class StatisticTest
{
    private static readonly IAsyncStatisticsApi StatisticsApi = new StatisticsWebApi();
    private static readonly Credential Credential = new() { Login = "roflan", Password = "Rofl123" };
    private const string Message = "Test message";

    [TestMethod]
    public async Task Test01GetTopPlayers()
    {
        var (success, players) = await StatisticsApi.TryGetTopPlayers();
        IsTrue(success);
        IsTrue(players.Any());
    }

    [TestMethod]
    public async Task Test02GetTopPlayersAuth()
    {
        var (success, players) = await StatisticsApi.TryGetTopPlayers(Credential);
        IsTrue(success);
        IsTrue(players.Any());
    }
}