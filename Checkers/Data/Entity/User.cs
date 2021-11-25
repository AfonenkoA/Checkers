using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Checkers.Data.Entity;


public sealed class UserCreationData
{
    public string Nick { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public enum UserType
{
    Player,
    Editor,
    Moderator,
    Support,
    Admin
}

public class PublicUserData 
{
    public static readonly PublicUserData Invalid = new();
    public int Id { get; set; } = -1;
    public string Nick { get; set; } = string.Empty;
    public int SocialCredit { get; set; } = -1;
    public int PictureId { get; set; } = -1;
    public DateTime LastActivity { get; set; } = DateTime.MinValue;

    public int SelectedCheckersId { get; set; } = -1;
    public int SelectedAnimationId { get; set; } = -1;

    public IEnumerable<int> Achievements { get; set; } = Enumerable.Empty<int>();

    [JsonIgnore] public bool IsValid => !(Id == -1 ||
                                          Nick == string.Empty ||
                                          SocialCredit == -1 ||
                                          PictureId == -1 ||
                                          LastActivity == DateTime.MinValue ||
                                          SelectedCheckersId == -1 ||
                                          SelectedAnimationId == -1);
}


public class FriendUserData : PublicUserData
{
    public new static readonly FriendUserData Invalid = new();

    public int CharId { get; set; } = -1;
}


public sealed class User : FriendUserData
{
    public new static readonly User Invalid = new();

    public IEnumerable<int> Items { get; set; } = Enumerable.Empty<int>();
    public IEnumerable<Friendship> Friends { get; set; } = Enumerable.Empty<Friendship>();
    public IEnumerable<int> Games { get; set; } = Enumerable.Empty<int>();
}