namespace Site.Data.Models.Post;

public sealed class PostView : PostPreview
{
    public string Content { get; }
    public DateTime Created { get; }
    public int PostId { get;}

    public PostView(Common.Entity.Article article) : base(article)
    {
        Content = article.Content;
        Created = article.Created;
        PostId = article.PostId;
    }
}