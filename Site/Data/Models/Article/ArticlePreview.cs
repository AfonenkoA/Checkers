using Common.Entity;

namespace Site.Data.Models.Article;

public class ArticlePreview
{
    public int Id { get; }
    public string Title { get; }
    public string Abstract { get; }
    public ResourceView Image { get; }
    public ArticlePreview(ArticleInfo info, ResourceView image)
    {
        Image = image;
        Id = info.Id;
        Title = info.Title;
        Abstract = info.Abstract;
    }
}
