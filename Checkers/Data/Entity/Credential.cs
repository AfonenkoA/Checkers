using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public sealed class Credential
{
    public string Login { get; init; } = InvalidString;
    public string Password { get; init; } = InvalidString;

    [JsonIgnore]
    public bool IsValid => !(Login == InvalidString ||
                             Password == InvalidString);
}