using Common.Entity;
using static System.String;

namespace Site.Data.Models.Article;

public class ArticlePreview : PictureView
{
    public int Id { get;  }
    public string Title { get;  }
    public string Abstract { get; }

    public ArticlePreview(ArticleInfo info,string pictureUrl) : base(pictureUrl)
    {
        Id = info.Id;
        Title = info.Title;
        Abstract = info.Abstract;
    }
}
