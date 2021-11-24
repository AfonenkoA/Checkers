using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public class ForumWebApi : WebApiBase, IAsyncForumApi
{
    public Task<bool> CreatePost(Credential credential, PostCreationData post)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdateTitle(Credential credential, int postId, string title)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdateContent(Credential credential, int postId, string content)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdatePicture(Credential credential, int postId, int imageId)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeletePost(Credential credential, int postId)
    {
        throw new System.NotImplementedException();
    }

    public Task<(bool, Post)> TryGetPost(int postId)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> CommentPost(Credential credential, int postId, string comment)
    {
        throw new System.NotImplementedException();
    }

    public Task<(bool, IEnumerable<PostInfo>)> TryGetPosts()
    {
        throw new System.NotImplementedException();
    }
}