namespace Site.Data.Models.Article;

public sealed class ArticleView : ArticlePreview
{
    public string Content { get; }
    public DateTime Created { get; }
    public int PostId { get; }

    public ArticleView(Common.Entity.Article article, string pictureUrl) : base(article, pictureUrl)
    {
        Content = article.Content;
        Created = article.Created;
        PostId = article.PostId;
    }
}