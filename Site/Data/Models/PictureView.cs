namespace Site.Data.Models;

public class PictureView : ResourceView
{
    public PictureView(ResourceView resource, int id) : base(resource)
    {
        Id = id;
    }
    public int Id { get; }

    public PictureView(PictureView pic) : base(pic)
    {
        Id = pic.Id;
    }
}