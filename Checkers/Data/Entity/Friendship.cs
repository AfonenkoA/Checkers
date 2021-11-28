using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;
namespace Checkers.Data.Entity;

public enum FriendshipState
{
    Accepted,
    Canceled,
    Waiting,
    Invalid
}

public sealed class Friendship
{
    public int Id { get; set; } = InvalidId;
    public FriendshipState State { get; set; } = FriendshipState.Invalid;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidId ||
                             State == FriendshipState.Invalid);
}