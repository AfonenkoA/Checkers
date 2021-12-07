using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace Checkers.Data.Entity;

public enum Side
{
    White,
    Black
}

public enum WinReason
{
    Surrender
}

public class StoredEvent
{
    public TimeSpan Time { get; set; }
    public Side Actor { get; set; }
}

public sealed class StoredMoveEvent : StoredEvent
{
    public int From { get; set; }
    public int To { get; set; }
}

public class EmoteEventCreationData : StoredEvent
{
    public int EmotionId { get; set; }
}

public class StoredEmoteEvent : StoredEvent
{
    public Emotion Emotion { get; set; }
}

public sealed class PlayerInfo
{
    public PublicUserData User { get; set;}
    public CheckersSkin CheckersSkin { get; set; }
    public Animation Animation { get; set; }
    public int StartSocialCredit { get; set; }
    public int SocialCreditChange { get; set; }
}

public class GameBase
{
    public DateTime Start { get; set; }
    public TimeSpan Duration { get; set; }
    public Side Winner { get; set; }
    public WinReason WinReason { get; set; }
    public IEnumerable<StoredMoveEvent> Moves { get; set; } = Empty<StoredMoveEvent>();
}

public class GameCreationData : GameBase
{
    public int BlackPlayerId;
    public int WhitePlayerId;
    public IEnumerable<EmoteEventCreationData> Emotions { get; set; } = Empty<EmoteEventCreationData>();
}

public sealed class Game : GameBase
{
    public int Id { get; set; }
    public PlayerInfo Black { get; set; }
    public PlayerInfo White { get; set; }
    public IEnumerable<StoredEmoteEvent> Emotions { get; set; }
}