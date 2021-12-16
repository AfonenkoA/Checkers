﻿using System.Text.Json.Serialization;
using static System.Linq.Enumerable;
using static Common.Entity.EntityValues;

namespace Common.Entity;


public sealed class UserCreationData
{
    public string Nick { get; init; } = InvalidString;
    public string Login { get; init; } = InvalidString;
    public string Password { get; init; } = InvalidString;
    public string Email { get; init; } = InvalidString;
}

public enum UserType
{
    Player = 1,
    Editor,
    Moderator,
    Support,
    Admin,
    Invalid
}

public class BasicUserData
{
    public static readonly BasicUserData Invalid = new();

    public int Id { get; init; } = InvalidInt;
    public string Nick { get; init; } = InvalidString;
    public int SocialCredit { get; init; } = InvalidInt;
    public DateTime LastActivity { get; init; } = InvalidDate;
    public UserType Type { get; init; } = UserType.Invalid;

    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidInt ||
                             Nick == InvalidString ||
                             SocialCredit == InvalidInt ||
                             LastActivity == InvalidDate ||
                             Type == UserType.Invalid);
}

public class PublicUserData : BasicUserData
{
    public new static readonly PublicUserData Invalid = new(BasicUserData.Invalid);
    public PublicUserData(BasicUserData data)
    {
        Id = data.Id;
        Nick = data.Nick;
        SocialCredit = data.SocialCredit;
        LastActivity = data.LastActivity;
        Type = data.Type;
    }
    [JsonConstructor]
    public PublicUserData() { }

    public IEnumerable<Achievement> Achievements { get; set; } = Empty<Achievement>();
    public Picture Picture { get; set; } = Picture.Invalid;
    public CheckersSkin SelectedCheckers { get; set; } = CheckersSkin.Invalid;
    public Animation SelectedAnimation { get; set; } = Animation.Invalid;

    [JsonIgnore] public override bool IsValid => base.IsValid;
}


public sealed class FriendUserData : PublicUserData
{
    public FriendUserData(PublicUserData data) : base(data)
    {
        Achievements = data.Achievements;
        SelectedAnimation = data.SelectedAnimation;
        SelectedCheckers = data.SelectedCheckers;
        Picture = data.Picture;
    }
    [JsonConstructor]
    public FriendUserData() { }
    public new static readonly FriendUserData Invalid = new(PublicUserData.Invalid);

    public int ChatId { get; set; } = InvalidInt;

    [JsonIgnore] public override bool IsValid => base.IsValid && ChatId != InvalidInt;
}


public sealed class User : BasicUserData
{

    public new static readonly User Invalid = new(PublicUserData.Invalid);

    public User(PublicUserData data)
    {
        Id = data.Id;
        Nick = data.Nick;
        SocialCredit = data.SocialCredit;
        LastActivity = data.LastActivity;
        Type = data.Type;
        Picture = data.Picture;
    }

    [JsonConstructor]
    public User() { }

    public Picture Picture { get; set; }
    public int Currency { get; set; }
    public IEnumerable<CheckersSkin> CheckerSkins { get; set; } = Empty<CheckersSkin>();
    public IEnumerable<Animation> Animations { get; set; } = Empty<Animation>();
    public IEnumerable<Friendship> Friends { get; set; } = Empty<Friendship>();

    public IEnumerable<Animation> AvailableAnimations { get; set; } = Empty<Animation>();
    public IEnumerable<CheckersSkin> AvailableCheckersSkins { get; set; } = Empty<CheckersSkin>();
    public IEnumerable<LootBox> AvailableLootBox { get; set; } = Empty<LootBox>();
    public IEnumerable<Achievement> AvailableAchievement { get; set; } = Empty<Achievement>();

    [JsonIgnore]
    public override bool IsValid => base.IsValid &&
                                    CheckerSkins.Any() &&
                                    Animations.Any();
}