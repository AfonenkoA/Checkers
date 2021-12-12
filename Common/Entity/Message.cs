using System.Text.Json.Serialization;
using static Common.Entity.EntityValues;

namespace Common.Entity;

public enum ChatType
{
    Public = 1,
    Private
}

public sealed class Message
{
    public int Id { get; init; } = InvalidInt;
    public int UserId { get; init; } = InvalidInt;
    public string Content { get; init; } = InvalidString;
    public DateTime SendTime { get; init; } = InvalidDate;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidInt ||
                             UserId == InvalidInt ||
                             Content == InvalidString ||
                             SendTime == InvalidDate);
}