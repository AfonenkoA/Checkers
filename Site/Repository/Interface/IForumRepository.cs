using Common.Entity;
using Site.Data.Models;
using Site.Data.Models.Post;

namespace Site.Repository.Interface;

public interface IForumRepository
{
    public Task<(bool, VisualPost)> GetPost(ICredential c, int postId);
    public Task<(bool, IEnumerable<Preview>)> GetPosts();

    public Task<bool> Create(CreationData creation);

    Task<bool> UpdateTitle(ICredential credential, int postId, string title);
    Task<bool> UpdateContent(ICredential credential, int postId, string content);
    Task<bool> UpdatePicture(PictureUpdateData data);
}