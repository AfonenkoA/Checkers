using System;
using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public enum ChatType
{
    Public,
    Private
}

public sealed class Message
{
    public int Id { get; init; } = InvalidId;
    public int UserId { get; init; } = InvalidId;
    public string Content { get; init; } = InvalidString;
    public DateTime SendTime { get; init; } = InvalidDate;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidId ||
                             UserId == InvalidId ||
                             Content == InvalidString ||
                             SendTime == InvalidDate);
}