using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface
{
    internal interface INewsRepository
    {
        bool CreateArticle(Credential credential, ArticleCreationData article);
        bool UpdateTitle(Credential credential, int id, string title);
        bool UpdateAbstract(Credential credential, int id, string @abstract);
        bool UpdateContent(Credential credential, int id, string content);
        bool UpdatePicture(Credential credential, int id, int pictureId);
        bool UpdatePost(Credential credential, int id, int postId);
        bool DeleteArticle(Credential credential, int articleId);
        Article TryGetArticle(int articleId);
        IEnumerable<ArticleInfo> TryGetNews();
    }
}
