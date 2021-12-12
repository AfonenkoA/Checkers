using Common.Entity;
using static System.Linq.Enumerable;
using static Common.Entity.EntityValues;

namespace GameModel;

public enum Side
{
    White,
    Black
}

public static class SideExt
{
    public static Side Inverse(this Side s) => s == Side.White ? Side.Black : Side.White;
}

public enum WinReason
{
    Surrender,
    TimeOut
}

public sealed class PlayerInfo
{
    public static readonly PlayerInfo Invalid = new();
    public PublicUserData User { get; set; } = PublicUserData.Invalid;
    public CheckersSkin CheckersSkin { get; set; } = CheckersSkin.Invalid;
    public Animation Animation { get; set; } = Animation.Invalid;
    public int StartSocialCredit { get; set; } = InvalidInt;
    public int SocialCreditChange { get; set; } = InvalidInt;
}

public class Game
{
    public DateTime Start { get; set; } = InvalidDate;
    public TimeSpan Duration { get; set; } = InvalidTime;
    public Side Winner { get; set; }
    public WinReason WinReason { get; set; }
    public PlayerInfo Black { get; set; } = PlayerInfo.Invalid;
    public PlayerInfo White { get; set; } = PlayerInfo.Invalid;
    public IEnumerable<TurnEvent> Turns { get; set; } = Empty<TurnEvent>();
    public IEnumerable<MoveEvent> Moves { get; set; } = Empty<MoveEvent>();
    public IEnumerable<EmoteEvent> Emotions { get; set; } = Empty<EmoteEvent>();
}

public class IdentifiableGame : Game
{
    public int Id { get; set; } = InvalidInt;
}
