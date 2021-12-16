namespace Site.Data.Models.Article;

public sealed class ArticleView : ArticlePreview
{
    public string Content { get; }
    public DateTime Created { get; }
    public int PostId { get;}

    public ArticleView(Common.Entity.Article article) : base(article)
    {
        Content = article.Content;
        Created = article.Created;
        PostId = article.PostId;
    }
}