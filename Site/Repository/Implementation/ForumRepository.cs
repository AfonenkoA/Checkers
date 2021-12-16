using Api.Interface;
using Common.Entity;
using Site.Data.Models.Post;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class ForumRepository : IForumRepository
{
    private readonly IAsyncForumApi _forumApi;
    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IAsyncResourceService _resourceService;
    public ForumRepository(IAsyncForumApi forumApi,
        IUserRepository userRepository,
        IChatRepository chatRepository, IAsyncResourceService resourceService)
    {
        _forumApi = forumApi;
        _userRepository = userRepository;
        _chatRepository = chatRepository;
        _resourceService = resourceService;
    }
    public async Task<(bool, PostView)> GetPost(Credential c, int postId)
    {
        var (_, data) = await _forumApi.TryGetPost(postId);
        var (_, user) = await _userRepository.GetUser(data.AuthorId);
        var (_, chat) = await _chatRepository.GetMessages(c, data.ChatId);
        return (true, new PostView(data, user, chat, _resourceService.GetFileUrl(data.PictureId)));
    }

    public async Task<(bool, IEnumerable<PostPreview>)> GetPosts()
    {
        var (success, data) = await _forumApi.TryGetPosts();
        return (success,
            data.Select(p => new PostPreview(p, _resourceService.GetFileUrl(p.PictureId))));
    }
}