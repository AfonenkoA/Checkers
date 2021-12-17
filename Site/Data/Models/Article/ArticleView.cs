namespace Site.Data.Models.Article;

public sealed class VisualArticle : Preview
{
    public string Content { get; }
    public DateTime Created { get; }
    public int PostId { get; }

    public VisualArticle(Common.Entity.Article article, ResourceView image) : base(article, image)
    {
        Content = article.Content;
        Created = article.Created;
        PostId = article.PostId;
    }
}