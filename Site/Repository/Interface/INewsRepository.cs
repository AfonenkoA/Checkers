using Site.Data.Models.Article;

namespace Site.Repository.Interface;

public interface INewsRepository
{
    public Task<(bool, IEnumerable<Preview>)> GetNews();
    public Task<(bool,VisualArticle)> GetArticle(int id);
}