using System;
using System.Text.Json.Serialization;

namespace Checkers.Data.Entity;

public enum FriendshipState
{
    Accepted,
    Canceled,
    Waiting,
}

public sealed class Friendship
{
    public int Id { get; set; } = -1;
    public FriendshipState State { get; set; } = FriendshipState.Waiting;
    public DateTime AcceptDate { get; set; } = DateTime.MinValue;

    [JsonIgnore] public bool IsValid => !(Id == -1 || AcceptDate != DateTime.MinValue);
}