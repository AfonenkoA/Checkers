using Common.Entity;

namespace Site.Data.Models.Post;

public class PostPreview
{
    public int Id { get; }
    public string Title { get; }
    public ResourceView Image { get; }

    public PostPreview(PostInfo info, ResourceView image)
    {
        Image = image;
        Id = info.Id;
        Title = info.Title;
    }
}
