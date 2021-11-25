using System;
using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

internal interface IChatRepository
{
    IEnumerable<Message> GetMessages(int chatId, DateTime from);
    bool CreateMessage(Credential credential, int chatId, string message);
}