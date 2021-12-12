using System.Net.Http.Json;
using Api.Interface;
using ApiContract;
using Common.Entity;
using static ApiContract.Action.ForumApiAction;
using static Common.CommunicationProtocol;

namespace Api.WebImplementation;

public sealed class ForumWebApi : WebApiBase, IAsyncForumApi
{
    public async Task<bool> CreatePost(Credential credential, PostCreationData post)
    {
        var route = Route.ForumRoute + Query(credential);
        using var response = await Client.PostAsJsonAsync(route, post);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTitle(Credential credential, int postId, string title)
    {
        var route = Route.ForumRoute + $"/{postId}/" + Query(credential, UpdatePostTitle);
        using var response = await Client.PutAsJsonAsync(route, title);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateContent(Credential credential, int postId, string content)
    {
        var route = Route.ForumRoute + $"/{postId}" + Query(credential, UpdatePostContent);
        using var response = await Client.PutAsJsonAsync(route, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePicture(Credential credential, int postId, int imageId)
    {
        var route = Route.ForumRoute + $"/{postId}" + Query(credential, UpdatePostPicture);
        using var response = await Client.PutAsJsonAsync(route, imageId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePost(Credential credential, int postId)
    {
        var route = Route.ForumRoute + $"/{postId}";
        using var response = await Client.DeleteAsync(route);
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, Post)> TryGetPost(int postId)
    {
        var route = Route.ForumRoute + $"/{postId}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<Post>(response);
        return res != null ? (true, res) : (false, Post.Invalid);
    }

    public async Task<(bool, IEnumerable<PostInfo>)> TryGetPosts()
    {
        var response = await Client.GetStringAsync(Route.ForumRoute);
        var res = Deserialize<List<PostInfo>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<PostInfo>());
    }
}