using System.Collections.Generic;

#nullable disable

namespace Checkers.Data.Old;

public sealed class ItemOption
{
    public ItemOption()
    {
        GamePlayer1Animations = new HashSet<Game>();
        GamePlayer1Chekers = new HashSet<Game>();
        GamePlayer2Animations = new HashSet<Game>();
        GamePlayer2Chekers = new HashSet<Game>();
        Items = new HashSet<Item>();
        UserSelectedAnimationNavigations = new HashSet<User>();
        UserSelectedCheckersNavigations = new HashSet<User>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Game> GamePlayer1Animations { get; set; }
    public ICollection<Game> GamePlayer1Chekers { get; set; }
    public ICollection<Game> GamePlayer2Animations { get; set; }
    public ICollection<Game> GamePlayer2Chekers { get; set; }
    public ICollection<Item> Items { get; set; }
    public ICollection<User> UserSelectedAnimationNavigations { get; set; }
    public ICollection<User> UserSelectedCheckersNavigations { get; set; }
}