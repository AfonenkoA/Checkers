using Common.Entity;

namespace Site.Data.Models.User;

public sealed class Friend : UserInfo
{
    public int ChatId { get; }

    public Friend(FriendUserData data,string pictureUrl) : base(data, pictureUrl)
    {
        ChatId = data.ChatId;
    }
}