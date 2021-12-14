using Api.Interface;
using Common.Entity;
using static ApiContract.Route;
using static Common.CommunicationProtocol;

namespace Api.WebImplementation;

public sealed class StatisticsWebApi : WebApiBase, IAsyncStatisticsApi
{
    public async Task<(bool, IDictionary<long, PublicUserData>)> TryGetTopPlayers()
    {
        const string route = $"{StatisticsRoute}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<IDictionary<long, PublicUserData>>(response);
        return res != null ? (true, res) : (false, new Dictionary<long,PublicUserData>());
    }

    public async Task<(bool, IDictionary<long, PublicUserData>)> TryGetTopPlayers(Credential credential)
    {
        var route = $"{StatisticsRoute}{Query(credential)}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<IDictionary<long, PublicUserData>>(response);
        return res != null ? (true, res) : (false, new Dictionary<long, PublicUserData>());
    }

    public string GetOnlineImageUrl()
    {
        throw new NotImplementedException();
    }
}