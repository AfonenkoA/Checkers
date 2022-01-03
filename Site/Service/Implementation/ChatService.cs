using Api.Interface;
using Common.Entity;
using Site.Data.Models;
using Site.Service.Interface;

namespace Site.Service.Implementation;

public sealed class ChatService : IChatService
{
    private readonly IAsyncChatApi _chatApi;
    private readonly IUserService _userRepository;

    public ChatService(IAsyncChatApi chatApi, IUserService userRepository)
    {
        _chatApi = chatApi;
        _userRepository = userRepository;
    }

    public async Task<(bool, Chat)> GetMessages(ICredential credential, int chatId)
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

    public Task<(bool, int)> GetCommonChatId(ICredential credential) => _chatApi.TryGetCommonChatId(credential);

    public Task<bool> SendMessage(ICredential credential, int chatId, string message) =>
        _chatApi.SendMessage(credential, chatId, message);
}