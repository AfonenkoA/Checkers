using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface INewsRepository
{
    bool CreateArticle(Credential credential, ArticleCreationData article);
    bool UpdateTitle(Credential credential, int id, string title);
    bool UpdateAbstract(Credential credential, int id, string @abstract);
    bool UpdateContent(Credential credential, int id, string content);
    bool UpdatePictureId(Credential credential, int id, int pictureId);
    bool UpdatePostId(Credential credential, int id, int postId);
    bool DeleteArticle(Credential credential, int articleId);
    Article GetArticle(int articleId);
    IEnumerable<ArticleInfo> GetNews();
}