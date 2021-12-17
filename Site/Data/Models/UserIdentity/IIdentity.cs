using Common.Entity;

namespace Site.Data.Models.UserIdentity;

public interface IIdentity : ICredential
{
    public int UserId { get; }
    public UserType Type { get; }
}