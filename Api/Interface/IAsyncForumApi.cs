using Common.Entity;

namespace Api.Interface;

public interface IAsyncForumApi
{
    Task<bool> CreatePost(Credential credential, PostCreationData post);
    Task<bool> UpdateTitle(Credential credential, int postId, string title);
    Task<bool> UpdateContent(Credential credential,int postId, string content);
    Task<bool> UpdatePicture(Credential credential,int postId, int imageId);
    Task<bool> DeletePost(Credential credential, int postId);
    Task<(bool,Post)> TryGetPost(int postId);
    Task<(bool,IEnumerable<PostInfo>)> TryGetPosts();
}