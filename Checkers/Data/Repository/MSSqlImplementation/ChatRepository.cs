using System;
using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;

namespace Checkers.Data.Repository.MSSqlImplementation
{
    public class ChatRepository : Repository, IChatRepository
    {
        public const string ChatTable = "[Chat]";

        public const string ChatId = "[chat_id]";
        public const string ChatName = "[chat_name]";

        public IEnumerable<Message> GetMessages(int chatId, DateTime from)
        {
            throw new NotImplementedException();
        }

        public bool CreateMessage(Credential credential, int chatId, string message)
        {
            throw new NotImplementedException();
        }
        
    }
}
