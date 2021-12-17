using static Common.Entity.EntityValues;

namespace Site.Data.Models;

public sealed class PictureView : ResourceView
{
    public static readonly PictureView Invalid = new PictureView(ResourceView.Invalid,InvalidInt);
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