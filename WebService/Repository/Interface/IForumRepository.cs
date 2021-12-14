using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IForumRepository
{
    bool CreatePost(Credential credential, PostCreationData post);
    bool UpdateTitle(Credential credential, int postId, string title);
    bool UpdateContent(Credential credential, int postId, string content);
    bool UpdatePictureId(Credential credential, int postId, int imageId);
    bool DeletePost(Credential credential, int postId);
    Post GetPost(int postId);
    IEnumerable<PostInfo> GetPosts();
}