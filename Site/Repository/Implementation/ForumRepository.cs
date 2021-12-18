using Api.Interface;
using Common.Entity;
using Site.Data.Models;
using Site.Data.Models.Post;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class ForumRepository : IForumRepository
{
    private readonly IAsyncForumApi _forumApi;
    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IResourceRepository _resource;
    public ForumRepository(IAsyncForumApi forumApi,
        IUserRepository userRepository,
        IChatRepository chatRepository,
        IResourceRepository resource)
    {
        _forumApi = forumApi;
        _userRepository = userRepository;
        _chatRepository = chatRepository;
        _resource = resource;
    }

    public async Task<(bool, VisualPost)> GetPost(ICredential c, int postId)
    {
        var (_, data) = await _forumApi.TryGetPost(postId);
        var (_, user) = await _userRepository.GetUser(data.AuthorId);
        var (_, chat) = await _chatRepository.GetMessages(c, data.ChatId);
        return (true, new VisualPost(data, user, chat, _resource.GetResource(data.PictureId)));
    }

    public async Task<(bool, IEnumerable<Preview>)> GetPosts()
    {
        var (success, data) = await _forumApi.TryGetPosts();
        return (success,
            data.Select(p => new Preview(p, _resource.GetResource(p.PictureId))));
    }

    public async Task<bool> Create(CreationData creation)
    {
        if (creation.Picture == null) return false;
        var pic = creation.Picture;
        if (creation.Login == null) return false;
        var login = creation.Login;
        if (creation.Password == null) return false;
        var password = creation.Password;
        if (creation.Title == null) return false;
        var title = creation.Title;
        if (creation.Content == null) return false;
        var content = creation.Content;
        var c = new Credential { Login = login, Password = password };
        var (s, id) = await _resource.Create(c, pic);
        if (!s) return false;
        var post = new PostCreationData { Content = content, PictureId = id, Title = title };
        return await _forumApi.CreatePost(c, post);
    }

    public Task<bool> UpdateTitle(ICredential credential, int postId, string title) =>
        _forumApi.UpdateTitle(credential, postId, title);

    public Task<bool> UpdateContent(ICredential credential, int postId, string content) =>
        _forumApi.UpdateContent(credential, postId, content);

    public async Task<bool> UpdatePicture(PictureUpdateData data)
    {
        if (data.Picture == null) return false;
        var (s, id) = await _resource.Create(data, data.Picture);
        if (!s) return false;
        return await _forumApi.UpdatePicture(data, data.Id, id);
    }

}