using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public sealed class AsyncResourceWebApi : WebApiBase, IAsyncResourceService
{
    public async Task<(bool, int)> TryUploadFile(Credential credential, byte[] picture, string ext)
    {
        var route = ResourceRoute + Query(credential);
        var value = Convert.ToBase64String(picture);
        var response = await Client.PostAsJsonAsync(route, value);
        var res = await response.Content.ReadAsStringAsync();
        var code = Deserialize<int>(res);
        return (true, code);
    }

    public string GetFileUrl(int id) => ResourceRoute + $"/{id}";
}