using System;
using System.Text.Json.Serialization;

namespace Checkers.Data.Entity;

public enum FriendState
{
    Accepted,
    Canceled,
    Waiting,
}

public sealed class Friend
{
    public int Id { get; set; } = -1;
    public FriendState State { get; set; } = FriendState.Waiting;
    public DateTime AcceptDateTime { get; set; } = DateTime.MinValue;

    [JsonIgnore] public bool IsValid => !(Id == -1 || AcceptDateTime != DateTime.MinValue);
}