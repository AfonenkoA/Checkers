using System.Net.Http.Json;
using Api.Interface;
using Common.Entity;
using static ApiContract.Route;
using static Common.CommunicationProtocol;

namespace Api.WebImplementation;

public sealed class AsyncResourceWebApi : WebApiBase, IAsyncResourceService
{
    public async Task<(bool, int)> TryUploadFile(ICredential credential, byte[] picture, string ext)
    {
        var route = $"{ResourceRoute}{Query(credential, ext)}";
        using var response = await Client.PostAsJsonAsync(route, Convert.ToBase64String(picture));
        var res = await response.Content.ReadAsStringAsync();
        var code = Deserialize<int>(res);
        return (true, code);
    }

    public async Task<(bool, byte[])> TryGetFile(int id)
    {
        var route = $"{ResourceRoute}/{id}";
        using var response = await Client.GetAsync(route);
        var res = await response.Content.ReadAsByteArrayAsync();
        return res.Any() ? (true, res) : (false, Array.Empty<byte>());
    }

    public string GetFileUrl(int id) => $"{Client.BaseAddress}{ResourceRoute}/{id}";
}