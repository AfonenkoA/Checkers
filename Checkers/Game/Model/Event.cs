using System;
using System.Text.Json.Serialization;
using Checkers.Data.Entity;
using Checkers.Game.Transmission;

namespace Checkers.Game.Model;

public interface IGameEvent
{}

public class TimeMarkedEvent : IGameEvent
{
    public TimeSpan Time { get; set; }
}

public class PersonalizedEvent : TimeMarkedEvent
{
    public Side Side { get; set; }
}

public sealed class MoveEvent : PersonalizedEvent
{
    public int From { get; set; }
    public int To { get; set; }
}

public sealed class EmoteEvent : PersonalizedEvent
{
    public Emotion Emotion { get; set; }
}

public sealed class TurnEvent : PersonalizedEvent
{ }

public sealed class GameStartEvent : IGameEvent
{
    public DateTime Start { get; set; }
    public PlayerInfo Black { get; set; }
    public PlayerInfo White { get; set; }
}

public sealed class ExceptionEvent : TimeMarkedEvent
{
    public string Message { get; set; }
}

public sealed class GameEndEvent : IGameEvent
{
    public DateTime Start { get; set; }
    public TimeSpan Duration { get; set; }
    public Side Winner { get; set; }
    public WinReason WinReason { get; set; }
}

public sealed class YourSideEvent : PersonalizedEvent
{}