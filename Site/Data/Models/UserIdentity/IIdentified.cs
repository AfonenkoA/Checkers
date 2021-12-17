namespace Site.Data.Models.UserIdentity;

public interface IIdentified<out T> : IIdentity
{
    public T Value { get; }
}