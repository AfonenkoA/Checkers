using Api.Interface;
using Common.Entity;
using Site.Data.Models;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class ChatRepository : IChatRepository
{
    private readonly IAsyncChatApi _chatApi;
    private readonly IUserRepository _userRepository;

    public ChatRepository(IAsyncChatApi chatApi, IUserRepository userRepository)
    {
        _chatApi = chatApi;
        _userRepository = userRepository;
    }

    public async Task<(bool, Chat)> GetMessages(Credential credential, int chatId)
    {
        var (success, data) = await _chatApi.TryGetMessages(credential, chatId, DateTime.MinValue);
        var messages = new List<MessageView>();
        foreach (var message in data)
        {
            var (_, user) = await _userRepository.GetUser(message.UserId);
            messages.Add(new MessageView(user,message));
        }
        return (success, new Chat(chatId,messages));
    }

    public Task<(bool, int)> GetCommonChatId(Credential credential) => _chatApi.TryGetCommonChatId(credential);

    public Task<bool> SendMessage(Credential credential, int chatId, string message) =>
        _chatApi.SendMessage(credential, chatId, message);
}