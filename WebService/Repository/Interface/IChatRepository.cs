using System;
using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IChatRepository
{
    IEnumerable<Message> GetMessages(Credential credential, int chatId, DateTime from);
    bool CreateMessage(Credential credential, int chatId, string message);
    int GetCommonChatId(Credential credential);
}