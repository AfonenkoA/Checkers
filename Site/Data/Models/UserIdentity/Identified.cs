namespace Site.Data.Models.UserIdentity;

public sealed class Identified<T> : Identity,  IIdentified<T>
{
    public Identified(IIdentity identity, T value)
    {
        Login = identity.Login;
        Password = identity.Password;
        Value = value;
        Type = identity.Type;
        UserId = identity.UserId;
    }

    public T Value { get; }
}