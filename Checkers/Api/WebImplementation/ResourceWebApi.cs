using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public sealed class AsyncResourceWebApi : WebApiBase, IAsyncResourceService
{
    public async Task<(bool, int)> TryUploadFile(Credential credential, byte[] picture, string ext)
    {
        var route = ResourceRoute + Query(credential,ext);
        var response = await Client.PostAsJsonAsync(route, Convert.ToBase64String(picture));
        var res = await response.Content.ReadAsStringAsync();
        var code = Deserialize<int>(res);
        return (true, code);
    }

    public async Task<(bool, byte[])> TryGetFile(int id)
    {
        var route = ResourceRoute +$"/{id}";
        var response = await Client.GetAsync(route);
        var res = await response.Content.ReadAsByteArrayAsync();
        return res.Any() ? (true, res) : (false, Array.Empty<byte>());
    }

    public string GetFileUrl(int id) => ResourceRoute + $"/{id}";
}