using System.Net.Http.Json;
using Api.Interface;
using ApiContract;
using Common.Entity;
using static ApiContract.Action.NewsApiAction;
using static Common.CommunicationProtocol;


namespace Api.WebImplementation;

public sealed class NewsWebApi : WebApiBase, IAsyncNewsApi
{
    public async Task<bool> CreateArticle(Credential credential, ArticleCreationData article)
    {
        var route = Route.NewsRoute + Query(credential);
        using var response = await Client.PostAsJsonAsync(route, article);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTitle(Credential credential, int id, string title)
    {
        var route = Route.NewsRoute + $"/{id}/" + Query(credential, UpdateArticleTitle);
        using var response = await Client.PutAsJsonAsync(route, title);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAbstract(Credential credential, int id, string @abstract)
    {
        var route = Route.NewsRoute + $"/{id}/" + Query(credential, UpdateArticleAbstract);
        using var response = await Client.PutAsJsonAsync(route, @abstract);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateContent(Credential credential, int id, string content)
    {
        var route = Route.NewsRoute + $"/{id}/" + Query(credential, UpdateArticleContent);
        using var response = await Client.PutAsJsonAsync(route, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePicture(Credential credential, int id, int pictureId)
    {
        var route = Route.NewsRoute + $"/{id}/" + Query(credential, UpdateArticlePictureId);
        using var response = await Client.PutAsJsonAsync(route, pictureId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteArticle(Credential credential, int articleId)
    {
        var route = Route.NewsRoute + $"/{articleId}" + Query(credential);
        using var response = await Client.DeleteAsync(route);
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, Article)> TryGetArticle(int articleId)
    {
        var route = Route.NewsRoute + $"/{articleId}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<Article>(response);
        return res != null ? (true, res) : (false, Article.Invalid);
    }

    public async Task<(bool, IEnumerable<ArticleInfo>)> TryGetNews()
    {
        var response = await Client.GetStringAsync(Route.NewsRoute);
        var res = Deserialize<List<ArticleInfo>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<ArticleInfo>());
    }
}