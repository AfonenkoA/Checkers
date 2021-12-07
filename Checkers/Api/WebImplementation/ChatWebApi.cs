using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static Checkers.CommunicationProtocol;

namespace Checkers.Api.WebImplementation;

public sealed class ChatWebApi : WebApiBase, IAsyncChatApi
{
    public async Task<(bool, IEnumerable<Message>)> TryGetMessages(Credential credential, int chatId, DateTime _)
    {
        var route = ChatRoute + $"/{chatId}" + Query(credential);
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<List<Message>>(response);
        return res != null ? (res.All(i => i.IsValid), res) : (false, Enumerable.Empty<Message>());
    }

    public async Task<(bool, int)> TryGetCommonChatId(Credential credential)
    {
        var route = ChatRoute + "/public" + Query(credential);
        var response = await Client.GetStringAsync(route);
        return (true,Deserialize<int>(response));
    }

    public async Task<bool> SendMessage(Credential credential, int chatId, string message)
    {
        var route = ChatRoute + $"/{chatId}" + Query(credential);
        using var response = await Client.PostAsJsonAsync(route, message);
        return response.IsSuccessStatusCode;
    }
}