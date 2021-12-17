using Common.Entity;

namespace Api.Interface;

public interface IAsyncNewsApi
{
    Task<bool> CreateArticle(ICredential credential, ArticleCreationData article);
    Task<bool> UpdateTitle(ICredential credential, int id, string title);
    Task<bool> UpdateAbstract(ICredential credential, int id, string @abstract);
    Task<bool> UpdateContent(ICredential credential, int id, string content);
    Task<bool> UpdatePicture(ICredential credential, int id, int pictureId);
    Task<bool> DeleteArticle(ICredential credential, int articleId);
    Task<(bool, Article)> TryGetArticle(int articleId);
    Task<(bool, IEnumerable<ArticleInfo>)> TryGetNews();
}