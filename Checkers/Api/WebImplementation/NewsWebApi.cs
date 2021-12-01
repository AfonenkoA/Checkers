using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static Checkers.Api.Interface.Action.NewsApiAction;


namespace Checkers.Api.WebImplementation;

public sealed class NewsWebApi : WebApiBase, IAsyncNewsApi
{
    public async Task<bool> CreateArticle(Credential credential, ArticleCreationData article)
    {
        var route = NewsRoute + Query(credential);
        var response = await Client.PostAsJsonAsync(route, article);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTitle(Credential credential, int id, string title)
    {
        var route = NewsRoute + $"/{id}/" + Query(credential, UpdateArticleTitle);
        var response = await Client.PutAsJsonAsync(route, title);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAbstract(Credential credential, int id, string @abstract)
    {
        var route = NewsRoute + $"/{id}/" + Query(credential, UpdateArticleAbstract);
        var response = await Client.PutAsJsonAsync(route, @abstract);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateContent(Credential credential, int id, string content)
    {
        var route = NewsRoute + $"/{id}/" + Query(credential, UpdateArticleContent);
        var response = await Client.PutAsJsonAsync(route, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePicture(Credential credential, int id, int pictureId)
    {
        var route = NewsRoute + $"/{id}/" + Query(credential, UpdateArticlePictureId);
        var response = await Client.PutAsJsonAsync(route, pictureId.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteArticle(Credential credential, int articleId)
    {
        var route = NewsRoute + $"/{articleId}" + Query(credential);
        var response = await Client.DeleteAsync(route);
        return response.IsSuccessStatusCode;
    }

    public async Task<(bool, Article)> TryGetArticle(int articleId)
    {
        var route = NewsRoute + $"/{articleId}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<Article>(response);
        return res != null ? (true, res) : (false, Article.Invalid);
    }

    public async Task<(bool, IEnumerable<ArticleInfo>)> TryGetNews()
    {
        var response = await Client.GetStringAsync(NewsRoute);
        var res = Deserialize<List<ArticleInfo>>(response);
        return res != null ? (true, res) : (false, Enumerable.Empty<ArticleInfo>());
    }
}