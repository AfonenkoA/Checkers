using Common.Entity;

namespace Site.Data.Models.User;

public sealed class Self : UserInfo
{
    public IEnumerable<Friend> Friends { get; init; } = Enumerable.Empty<Friend>();
    public Self(UserInfo info) : base(info)
    {
    }
}