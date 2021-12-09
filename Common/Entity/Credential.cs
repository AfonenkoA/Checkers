using System.Text.Json.Serialization;
using static Common.Entity.EntityValues;

namespace Common.Entity;

public sealed class Credential
{
    public static readonly Credential Invalid = new();
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