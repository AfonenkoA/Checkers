using Common.Entity;
using Site.Data.Models;

namespace Site.Service.Interface;

public interface IChatService
{
    Task<(bool, Chat)> GetMessages(ICredential credential, int chatId);
    Task<(bool, int)> GetCommonChatId(ICredential credential);
    Task<bool> SendMessage(ICredential credential, int chatId, string message);
}