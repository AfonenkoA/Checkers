using Common.Entity;
using Site.Data.Models.User;
using static Common.Entity.EntityValues;

namespace Site.Data.Models.UserIdentity;

public class Identity : IIdentity
{
    public static readonly Identity Invalid = new(Credential.Invalid, UserInfo.Invalid);
    public Identity()
    { }
    public Identity(ICredential c, UserInfo self)
    {
        Login = c.Login;
        Password = c.Password;
        Type = self.Type;
        UserId = self.Id;
    }

    public int UserId { get; set; } = InvalidInt;
    public UserType Type { get; set; } = UserType.Invalid;
    public string Login { get; set; } = InvalidString;
    public string Password { get; set; } = InvalidString;

    public bool IsValid => !(Login == InvalidString ||
                             Password == InvalidString ||
                             Type == UserType.Invalid ||
                             UserId == InvalidInt);
}