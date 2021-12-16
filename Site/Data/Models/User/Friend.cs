using Common.Entity;

namespace Site.Data.Models.User;

public sealed class Friend : UserInfo
{
    public int ChatId { get; }

    public Friend(FriendUserData data, PictureView picture) : base(data, picture)
    {
        ChatId = data.ChatId;
    }
}