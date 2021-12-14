using Common.Entity;

namespace Api.Interface;

public interface IAsyncNewsApi
{
    Task<bool> CreateArticle(Credential credential, ArticleCreationData article);
    Task<bool> UpdateTitle(Credential credential, int id, string title);
    Task<bool> UpdateAbstract(Credential credential, int id, string @abstract);
    Task<bool> UpdateContent(Credential credential, int id, string content);
    Task<bool> UpdatePicture(Credential credential, int id, int pictureId);
    Task<bool> DeleteArticle(Credential credential, int articleId);
    Task<(bool, Article)> TryGetArticle(int articleId);
    Task<(bool, IEnumerable<ArticleInfo>)> TryGetNews();
}