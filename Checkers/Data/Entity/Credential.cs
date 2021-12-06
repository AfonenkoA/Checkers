using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public sealed class Credential
{
    public string Login { get; set; } = InvalidString;
    public string Password { get; set; } = InvalidString;

    [JsonIgnore]
    public bool IsValid => !(Login == InvalidString ||
                             Password == InvalidString);

    public override string ToString()
    {
        return $"l:{Login} p:{Password}";
    }
}