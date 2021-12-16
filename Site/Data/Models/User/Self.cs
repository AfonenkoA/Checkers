using Common.Entity;

namespace Site.Data.Models.User;

public sealed class Self : UserInfo
{
    public IEnumerable<Friend> Friends { get; init; } = Enumerable.Empty<Friend>();
    public Self(BasicUserData data, string pictureUrl) : base(data, pictureUrl)
    {
    }
}