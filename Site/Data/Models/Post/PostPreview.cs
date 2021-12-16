using Common.Entity;
using static System.String;

namespace Site.Data.Models.Post;

public class PostPreview
{
    public int Id { get;  }
    public string Title { get;  }
    public string Abstract { get; }
    public string PictureUrl { get; init; } = Empty;

    public PostPreview(ArticleInfo info)
    {
        Id = info.Id;
        Title = info.Title;
        Abstract = info.Abstract;
    }
}
