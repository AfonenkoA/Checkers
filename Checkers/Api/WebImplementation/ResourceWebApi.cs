using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Api.WebImplementation;

public sealed class AsyncResourceWebApi : WebApiBase, IAsyncResourceService
{
    //не факт, что так
    public Task<(bool, int)> TryUploadPicture(Credential credential, byte[] picture, string ext) =>
        Client.PostAsJsonAsync(ResourceRoute + Query(credential, ResourceApiAction.Upload, ext),
            Convert.ToBase64String(picture))
            .ContinueWith(task => task.Result.Content.ReadAsStringAsync())
            .Unwrap()
            .ContinueWith(task => (true, Deserialize<int>(task.Result)));

    public string GetPictureUrl(int id) => ResourceRoute + $"/{id}";
}