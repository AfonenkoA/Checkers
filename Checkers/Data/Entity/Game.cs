using System;
using System.Collections.Generic;
using Checkers.Server;

namespace Checkers.Data.Entity;

public enum WinReason
{
    Surrender
}

public class ActionBase
{
    public TimeSpan Time { get; set; }
    public Color Actor { get; set; }
}

public class MoveData : ActionBase
{
    public int From { get; set; }
    public int To { get; set; }
}

public class EmoteCreationData : ActionBase
{
    public int EmotionId { get; set; }
}

public class Emotion
{}

public class EmoteData : ActionBase
{
    public Emotion Emotion { get; set; }
}

public class PlayerInfo
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
    public Color Winner { get; set; }
    public WinReason WinReason { get; set; }
    public IEnumerable<MoveData> Moves { get; set; }
}

public class GameCreationData : GameBase
{
    public int BlackId;
    public int WhiteId;
    public IEnumerable<EmoteCreationData> Emotions { get; set; }
}

public class GameData : GameBase
{
    public PlayerInfo Black;
    public PlayerInfo White;
    public IEnumerable<EmoteData> Emotions { get; set; }
}