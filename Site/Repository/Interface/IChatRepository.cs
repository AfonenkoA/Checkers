using Common.Entity;
using Site.Data.Models;

namespace Site.Repository.Interface;

public interface IChatRepository
{
    Task<(bool, Chat)> GetMessages(Credential credential, int chatId);
    Task<(bool, int)> GetCommonChatId(Credential credential);
    Task<bool> SendMessage(Credential credential, int chatId, string message);
}