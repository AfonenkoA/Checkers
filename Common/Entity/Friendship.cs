using System.Text.Json.Serialization;
using static Common.Entity.EntityValues;
namespace Common.Entity;

public enum FriendshipState
{
    Accepted=1,
    Canceled,
    Waiting,
    Invalid
}

public sealed class Friendship
{
    public int Id { get; init; } = InvalidInt;
    public FriendshipState State { get; init; } = FriendshipState.Invalid;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidInt ||
                             State == FriendshipState.Invalid);
}