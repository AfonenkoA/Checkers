using System;
using System.Collections.Generic;

#nullable disable

namespace Checkers.Data;

public sealed class Game
{
    public Game()
    {
        GamesProgresses = new HashSet<GamesProgress>();
    }

    public int Id { get; set; }
    public int Player1Id { get; set; }
    public int Player2Id { get; set; }
    public int Player1ChekersId { get; set; }
    public int Player2ChekersId { get; set; }
    public int Player1AnimationId { get; set; }
    public int Player2AnimationId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int WinnerId { get; set; }
    public int Player1RaitingChange { get; set; }
    public int Player2RaitingChange { get; set; }

    public User Player1 { get; set; }
    public ItemOption Player1Animation { get; set; }
    public ItemOption Player1Chekers { get; set; }
    public User Player2 { get; set; }
    public ItemOption Player2Animation { get; set; }
    public ItemOption Player2Chekers { get; set; }
    public User Winner { get; set; }
    public ICollection<GamesProgress> GamesProgresses { get; set; }
}