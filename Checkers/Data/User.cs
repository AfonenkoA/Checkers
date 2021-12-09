using System;
using System.Collections.Generic;

#nullable disable

namespace Checkers.Data;

public class User
{
    public User()
    {
        Achievements = new HashSet<Achievement>();
        FriendFriendNavigations = new HashSet<Friend>();
        FriendUsers = new HashSet<Friend>();
        GamePlayer1s = new HashSet<Game>();
        GamePlayer2s = new HashSet<Game>();
        GameWinners = new HashSet<Game>();
        GamesProgresses = new HashSet<GamesProgress>();
        Items = new HashSet<Item>();
    }

    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Nick { get; set; }
    public int Rating { get; set; }
    public int Currency { get; set; }
    public int PictureId { get; set; }
    public int SelectedCheckers { get; set; }
    public int SelectedAnimation { get; set; }
    public DateTime LastActivity { get; set; }

    public PictureOption Picture { get; set; }
    public ItemOption SelectedAnimationNavigation { get; set; }
    public ItemOption SelectedCheckersNavigation { get; set; }
    public ICollection<Achievement> Achievements { get; set; }
    public ICollection<Friend> FriendFriendNavigations { get; set; }
    public ICollection<Friend> FriendUsers { get; set; }
    public ICollection<Game> GamePlayer1s { get; set; }
    public ICollection<Game> GamePlayer2s { get; set; }
    public ICollection<Game> GameWinners { get; set; }
    public ICollection<GamesProgress> GamesProgresses { get; set; }
    public ICollection<Item> Items { get; set; }
}