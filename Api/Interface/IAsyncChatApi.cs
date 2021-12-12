using Common.Entity;

namespace Api.Interface;

public interface IAsyncChatApi
{
    Task<(bool,IEnumerable<Message>)> TryGetMessages(Credential credential,int chatId, DateTime from);
    Task<(bool, int)> TryGetCommonChatId(Credential credential);
    Task<bool> SendMessage(Credential credential,int chatId, string message);
}