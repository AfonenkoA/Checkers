using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public sealed class ChatWebApi : WebApiBase, IAsyncChatApi
{
    public Task<(bool, IEnumerable<Message>)> TryGetMessages(Credential credential,int chatId, DateTime _)=>
    Client.GetStringAsync(ChatRoute+$"/{chatId}"+Query(credential))
            .ContinueWith(task => Deserialize<List<Message>>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (res.All(i => i.IsValid), res) :
                    (false, Enumerable.Empty<Message>());
            });

    public Task<bool> SendMessage(Credential credential, int chatId, string message) =>
        Client.PostAsJsonAsync(ChatRoute + $"/{chatId}" + Query(credential), message)
            .ContinueWith(task=>task.Result.IsSuccessStatusCode);
}