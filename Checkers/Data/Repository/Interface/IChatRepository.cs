using System;
using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

public interface IChatRepository
{
    IEnumerable<Message> GetMessages(Credential credential,int chatId, DateTime from);
    bool CreateMessage(Credential credential, int chatId, string message);
    int GetCommonChatId(Credential credential);
}