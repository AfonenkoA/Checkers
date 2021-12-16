using Common.Entity;

namespace Site.Data.Models;

public interface IIdentified<out T>
{
    public string Login { get; }
    public string Password { get; }
    public T Value { get; }
}

public sealed class Identified<T> : IIdentified<T>
{
    public Identified(Credential credential, T value)
    {
        Login = credential.Login;
        Password = credential.Password;
        Value = value;
    }
    
    public string Login { get; }
    public string Password { get; }
    public T Value { get; }
}
