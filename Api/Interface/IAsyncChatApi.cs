using Common.Entity;

namespace Api.Interface;

public interface IAsyncChatApi
{
    Task<(bool, IEnumerable<Message>)> TryGetMessages(ICredential credential, int chatId, DateTime from);
    Task<(bool, int)> TryGetCommonChatId(ICredential credential);
    Task<bool> SendMessage(ICredential credential, int chatId, string message);
}