using Common.Entity;

namespace Site.Data.Models.UserIdentity;


public interface IIdentity : ICredential
{
    public UserType Type { get; }
}