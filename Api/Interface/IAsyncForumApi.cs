using Common.Entity;

namespace Api.Interface;

public interface IAsyncForumApi
{
    Task<bool> CreatePost(ICredential credential, PostCreationData post);
    Task<bool> UpdateTitle(ICredential credential, int postId, string title);
    Task<bool> UpdateContent(ICredential credential, int postId, string content);
    Task<bool> UpdatePicture(ICredential credential, int postId, int imageId);
    Task<bool> DeletePost(ICredential credential, int postId);
    Task<(bool, Post)> TryGetPost(int postId);
    Task<(bool, IEnumerable<PostInfo>)> TryGetPosts();
}