using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public sealed class Credential
{
    public string Login { get; set; } = InvalidString;
    public string Password { get; set; } = InvalidString;

    public Credential(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public Credential()
    { }

    [JsonIgnore]
    public bool IsValid => !(Login == InvalidString ||
                             Password == InvalidString);
}