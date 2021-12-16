using Site.Data.Models;
using Site.Data.Models.Article;

namespace Site.Repository.Interface;

public interface INewsRepository
{
    public Task<(bool, IEnumerable<ArticlePreview>)> GetNews();
    public Task<(bool,ArticleView)> GetArticle(int id);
}