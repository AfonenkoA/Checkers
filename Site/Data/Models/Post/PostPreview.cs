using Common.Entity;

namespace Site.Data.Models.Post;

public class PostPreview : PictureView
{
    public int Id { get;  }
    public string Title { get;  }

    public PostPreview(PostInfo info,string pictureUrl) : base(pictureUrl)
    {
        Id = info.Id;
        Title = info.Title;
    }
}
