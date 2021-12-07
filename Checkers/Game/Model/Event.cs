using System;
using System.Text.Json.Serialization;
using Checkers.Data.Entity;

namespace Checkers.Game.Model;

public class PersonalizedEvent
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

public sealed class GameStartEvent : PersonalizedEvent
{
    public PublicUserData Black { get; set; }
    public PublicUserData White { get; set; }

    [JsonConstructor]
    public GameStartEvent() { }

    public GameStartEvent(Side side)
    {
        Side = side;
    }
}

public sealed class ExceptionEvent
{
    public string Message { get; set; }
}

public sealed class GameEndEvent
{
    public DateTime Start { get; set; }
    public TimeSpan Duration { get; set; }
    public Side Winner { get; set; }
    public WinReason WinReason { get; set; }
}