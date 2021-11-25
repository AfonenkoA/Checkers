using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

public interface IForumRepository
{
    bool CreatePost(Credential credential, PostCreationData post);
    bool UpdateTitle(Credential credential, int postId, string title);
    bool UpdateContent(Credential credential, int postId, string content);
    bool UpdatePicture(Credential credential, int postId, int imageId);
    bool DeletePost(Credential credential, int postId);
    Post GetPost(int postId);
    bool CommentPost(Credential credential, int postId, string comment);
    IEnumerable<PostInfo> GetPosts();
}