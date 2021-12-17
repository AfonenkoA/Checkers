using Common.Entity;

namespace Site.Data.Models.UserIdentity;

public sealed class Identified<T> : IIdentified<T>
{
    public Identified(ICredential credential, T value, UserType type)
    {
        Login = credential.Login;
        Password = credential.Password;
        Value = value;
        Type = type;
    }

    public Identified(ICredential i, T value)
    {
        Value = value;
        Login = i.Login;
        Password = i.Password;
    }

    public string Login { get; }
    public string Password { get; }
    public UserType Type { get; }
    public T Value { get; }
}