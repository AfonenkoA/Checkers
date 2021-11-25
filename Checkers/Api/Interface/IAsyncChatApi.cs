using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncChatApi
{
    Task<(bool,IEnumerable<Message>)> TryGetMessages(Credential credential,int chatId, DateTime from);
    Task<bool> SendMessage(Credential credential,int chatId, string message);
}