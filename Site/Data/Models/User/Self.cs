namespace Site.Data.Models.User;

public sealed class Self : UserInfo
{
    public IEnumerable<Friend> Friends { get; } 
    public IEnumerable<PictureView> Pictures { get;}
    public Self(UserInfo info, 
        PictureView picture,
        IEnumerable<Friend> friends,
        IEnumerable<PictureView> pictures) : base(info,picture)
    {
        Friends = friends;
        Pictures = pictures;
    }
}