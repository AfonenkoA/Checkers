using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static Checkers.Api.Interface.Action.NewsApiAction;


namespace Checkers.Api.WebImplementation;

public sealed class NewsWebApi : WebApiBase,  IAsyncNewsApi
{
    public Task<bool> CreateArticle(Credential credential, ArticleCreationData article) =>
        Client.PostAsJsonAsync(NewsRoute + Query(credential), article)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateTitle(Credential credential, int id, string title) =>
        Client.PutAsJsonAsync(NewsRoute + $"/{id}" + Query(credential,UpdateArticleTitle), title)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateAbstract(Credential credential, int id, string @abstract) =>
        Client.PutAsJsonAsync(NewsRoute + $"/{id}" + Query(credential, UpdateArticleAbstract), @abstract)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdateContent(Credential credential, int id, string content) =>
        Client.PutAsJsonAsync(NewsRoute + $"/{id}" + Query(credential, UpdateArticleContent), content)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

    public Task<bool> UpdatePicture(Credential credential, int id, int pictureId) =>
        Client.PutAsJsonAsync(NewsRoute + $"/{id}" + Query(credential, UpdateArticlePictureId), pictureId)
            .ContinueWith(task => task.Result.IsSuccessStatusCode);

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