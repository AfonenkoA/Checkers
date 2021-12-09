using System.Net.Http.Json;
using Api.Interface;
using ApiContract;
using Common.Entity;
using static Common.CommunicationProtocol;

namespace Api.WebImplementation;

public sealed class ChatWebApi : WebApiBase, IAsyncChatApi
{
    public async Task<(bool, IEnumerable<Message>)> TryGetMessages(Credential credential, int chatId, DateTime _)
    {
        var route = Route.ChatRoute + $"/{chatId}" + Query(credential);
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<List<Message>>(response);
        return res != null ? (res.All(i => i.IsValid), res) : (false, Enumerable.Empty<Message>());
    }

    public async Task<(bool, int)> TryGetCommonChatId(Credential credential)
    {
        var route = Route.ChatRoute + "/public" + Query(credential);
        var response = await Client.GetStringAsync(route);
        return (true,Deserialize<int>(response));
    }

    public async Task<bool> SendMessage(Credential credential, int chatId, string message)
    {
        var route = Route.ChatRoute + $"/{chatId}" + Query(credential);
        using var response = await Client.PostAsJsonAsync(route, message);
        return response.IsSuccessStatusCode;
    }
}