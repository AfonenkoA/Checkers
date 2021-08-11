using System;
using System.Collections.Generic;

#nullable disable

namespace Checkers.Data
{
    public partial class User
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

        public virtual PictureOption Picture { get; set; }
        public virtual ItemOption SelectedAnimationNavigation { get; set; }
        public virtual ItemOption SelectedCheckersNavigation { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Friend> FriendFriendNavigations { get; set; }
        public virtual ICollection<Friend> FriendUsers { get; set; }
        public virtual ICollection<Game> GamePlayer1s { get; set; }
        public virtual ICollection<Game> GamePlayer2s { get; set; }
        public virtual ICollection<Game> GameWinners { get; set; }
        public virtual ICollection<GamesProgress> GamesProgresses { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
