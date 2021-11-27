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
}