using Common.Entity;
using Site.Data.Models.Post;

namespace Site.Repository.Interface;

public interface IForumRepository
{
    public Task<(bool, PostView)> GetPost(ICredential c,int postId);
    public Task<(bool, IEnumerable<PostPreview>)> GetPosts();
}