using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static Checkers.Api.Interface.Action.ForumApiAction;

namespace Checkers.Api.WebImplementation;

public sealed class ForumWebApi : WebApiBase, IAsyncForumApi
{
    public Task<bool> CreatePost(Credential credential, PostCreationData post) =>
        Client.PutAsJsonAsync(ForumRoute + Query(credential), post)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateTitle(Credential credential, int postId, string title) =>
        Client.PutAsJsonAsync(ForumRoute + $"/{postId}" + Query(credential, UpdatePostTitle), title)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateContent(Credential credential, int postId, string content) =>
        Client.PutAsJsonAsync(ForumRoute + $"/{postId}" + Query(credential, UpdatePostContent), content)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdatePicture(Credential credential, int postId, int imageId) =>
        Client.PutAsJsonAsync(ForumRoute + $"/{postId}" + Query(credential, UpdatePostPicture), imageId)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> DeletePost(Credential credential, int postId) =>
        Client.DeleteAsync(ForumRoute + $"/{postId}")
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<(bool, Post)> TryGetPost(int postId) =>
        Client.GetStringAsync(ForumRoute + $"/{postId}")
        .ContinueWith(task => Deserialize<Post>(task.Result))
        .ContinueWith(task =>
    {
        var res = task.Result;
        return res != null ? (true, res) : (false, Post.Invalid);
    });

    public Task<(bool, IEnumerable<PostInfo>)> TryGetPosts() =>
        Client.GetStringAsync(ForumRoute)
            .ContinueWith(task => Deserialize<List<PostInfo>>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, Enumerable.Empty<PostInfo>());
            });
}