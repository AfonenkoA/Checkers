using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Api.WebImplementation;

public sealed class NewsWebApi : WebApiBase,  IAsyncNewsApi
{
    public Task<bool> CreateArticle(Credential credential, ArticleCreationData article) =>
        Client.PostAsJsonAsync(NewsRoute + Query(credential), article)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateTitle(Credential credential, int id, string title)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdateAbstract(Credential credential, int id, string @abstract)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdateContent(Credential credential, int id, string content)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdatePicture(Credential credential, int id, int pictureId)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdatePost(Credential credential, int id, int postId)
    {
        throw new System.NotImplementedException();
    }


    public Task<bool> DeleteArticle(Credential credential, int articleId) =>
        Client.DeleteAsync(NewsRoute + $"/{articleId}" + Query(credential))
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<(bool, Article)> TryGetArticle(int articleId) =>
        Client.GetStringAsync(NewsRoute + $"/{articleId}")
            .ContinueWith(task => Deserialize<Article>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, Article.Invalid);
            });

    public Task<(bool, IEnumerable<ArticleInfo>)> TryGetNews() =>
        Client.GetStringAsync(NewsRoute)
            .ContinueWith(task => Deserialize<IEnumerable<ArticleInfo>>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, Enumerable.Empty<ArticleInfo>());
            });
    }