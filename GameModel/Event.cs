using Common.Entity;
using static System.String;
using static Common.Entity.Emotion;
using static Common.Entity.EntityValues;

namespace GameModel;

public interface IGameEvent
{}

public class TimeMarkedEvent : IGameEvent
{
    public TimeSpan Time { get; set; } = InvalidTime;
}

public class PersonalizedEvent : TimeMarkedEvent
{
    public Side Side { get; set; }
}

public sealed class MoveEvent : PersonalizedEvent
{
    public int From { get; set; } = InvalidInt;
    public int To { get; set; } = InvalidInt;
}

public sealed class EmoteEvent : PersonalizedEvent
{
    public Emotion Emotion { get; set; } = Invalid;
}

public sealed class TurnEvent : PersonalizedEvent
{ }

public sealed class GameStartEvent : IGameEvent
{
    public DateTime Start { get; set; }
    public PlayerInfo Black { get; set; } = PlayerInfo.Invalid;
    public PlayerInfo White { get; set; } = PlayerInfo.Invalid;
}

public sealed class ExceptionEvent : TimeMarkedEvent
{
    public string Message { get; set; } = Empty;
}

public sealed class GameEndEvent : IGameEvent
{
    public DateTime Start { get; set; } = InvalidDate;
    public TimeSpan Duration { get; set; } = InvalidTime;
    public Side Winner { get; set; }
    public WinReason WinReason { get; set; }
}

public sealed class YourSideEvent : PersonalizedEvent
{}