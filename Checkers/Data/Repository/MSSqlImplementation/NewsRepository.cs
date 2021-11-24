using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;

namespace Checkers.Data.Repository.MSSqlImplementation;

internal class NewsRepository: Repository, INewsRepository
{
    public bool CreateArticle(Credential credential, ArticleCreationData article)
    {
        throw new System.NotImplementedException();
    }

    public bool UpdateTitle(Credential credential, int id, string title)
    {
        throw new System.NotImplementedException();
    }

    public bool UpdateAbstract(Credential credential, int id, string @abstract)
    {
        throw new System.NotImplementedException();
    }

    public bool UpdateContent(Credential credential, int id, string content)
    {
        throw new System.NotImplementedException();
    }

    public bool UpdatePicture(Credential credential, int id, int pictureId)
    {
        throw new System.NotImplementedException();
    }

    public bool UpdatePost(Credential credential, int id, int postId)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteArticle(Credential credential, int articleId)
    {
        throw new System.NotImplementedException();
    }

    public Article TryGetArticle(int articleId)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<ArticleInfo> TryGetNews()
    {
        throw new System.NotImplementedException();
    }
    
}