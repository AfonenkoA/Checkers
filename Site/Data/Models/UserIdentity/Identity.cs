using Common.Entity;
using static System.String;

namespace Site.Data.Models.UserIdentity;

public sealed class Identity :  IIdentity
{
    public Identity()
    {
        Login = Credential.Invalid.Login;
        Password = Credential.Invalid.Password;
        Type = UserType.Invalid;
    }
    public Identity(ICredential c, UserType type)
    {
        Login = c.Login;
        Password = c.Password;
        Type = type;
    }
    public Identity(string login, string password, UserType type)
    {
        Login = login;
        Password = password;
        Type = type;
    }
    
    public UserType Type { get; set; }
    public static readonly Identity Invalid = new(Credential.Invalid, UserType.Invalid);
    public string Login { get; set; }
    public string Password { get; set; }

    public bool IsValid => !(Login == Empty ||
                             Password == Empty ||
                             Type == UserType.Invalid);
}