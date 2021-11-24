using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public class ChatWebApi : WebApiBase, IAsyncChatApi
{
    public Task<(bool, IEnumerable<Message>)> TryGetMessages(int chatId, DateTime from)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendMessage(Credential credential, int chatId, string message)
    {
        throw new NotImplementedException();
    }
}