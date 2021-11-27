using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public sealed class AsyncResourceWebApi : WebApiBase, IAsyncResourceService
{
    public Task<(bool, int)> TryUploadFile(Credential credential, byte[] picture, string ext) =>
        Client.PostAsJsonAsync(ResourceRoute + Query(credential),
            Convert.ToBase64String(picture))
            .ContinueWith(task => task.Result.Content.ReadAsStringAsync())
            .Unwrap()
            .ContinueWith(task => (true, Deserialize<int>(task.Result)));

    public string GetFileUrl(int id) => ResourceRoute + $"/{id}";
}