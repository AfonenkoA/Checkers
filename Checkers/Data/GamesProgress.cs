using System;

#nullable disable

namespace Checkers.Data;

public sealed class GamesProgress
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int ActionNum { get; set; }
    public int ActorId { get; set; }
    public int ActionId { get; set; }
    public string ActionDesc { get; set; }
    public TimeSpan ActionTime { get; set; }

    public EventOption Action { get; set; }
    public User Actor { get; set; }
    public Game Game { get; set; }
}