using System;
using System.Collections.Generic;
using Checkers.Data.Entity;
using static System.Linq.Enumerable;

namespace Checkers.Game.Model;

public enum Side
{
    White,
    Black
}

internal static class SideExt
{
    internal static Side Inverse(this Side s) => s == Side.White ? Side.Black : Side.White;
}

public enum WinReason
{
    Surrender,
    TimeOut
}

public sealed class PlayerInfo
{
    public PublicUserData User { get; set;}
    public CheckersSkin CheckersSkin { get; set; }
    public Animation Animation { get; set; }
    public int StartSocialCredit { get; set; }
    public int SocialCreditChange { get; set; }
}

public sealed class GameData
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public TimeSpan Duration { get; set; }
    public Side Winner { get; set; }
    public WinReason WinReason { get; set; }
    public PlayerInfo Black { get; set; }
    public PlayerInfo White { get; set; }
    public IEnumerable<TurnEvent> Turns { get; set; } = Empty<TurnEvent>();
    public IEnumerable<MoveEvent> Moves { get; set; } = Empty<MoveEvent>();
    public IEnumerable<EmoteEvent> Emotions { get; set; }
}