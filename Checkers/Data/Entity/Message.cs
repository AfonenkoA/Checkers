using System;

namespace Checkers.Data.Entity;

public enum ChatType
{
    Public,
    Private
}

public sealed class Message
{
    public int Id { get; set; } = -1;
    public int UserId { get; set; } = -1;
    public string Content { get; set; } = string.Empty;
    public DateTime SendTime { get; set; } = DateTime.MinValue;
}