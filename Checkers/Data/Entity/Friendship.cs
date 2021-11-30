using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;
namespace Checkers.Data.Entity;

public enum FriendshipState
{
    Accepted=1,
    Canceled,
    Waiting,
    Invalid
}

public sealed class Friendship
{
    public int Id { get; init; } = InvalidId;
    public FriendshipState State { get; init; } = FriendshipState.Invalid;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidId ||
                             State == FriendshipState.Invalid);
}