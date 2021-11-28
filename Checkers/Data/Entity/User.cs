using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;


public sealed class UserCreationData
{
    public string Nick { get; set; } = InvalidString;
    public string Login { get; set; } = InvalidString;
    public string Password { get; set; } = InvalidString;
    public string Email { get; set; } = InvalidString;
}

public enum UserType
{
    Player,
    Editor,
    Moderator,
    Support,
    Admin,
    Invalid
}

public class BasicUserData
{
    public static readonly BasicUserData Invalid = new();

    public int Id { get; init; } = InvalidId;
    public string Nick { get; init; } = InvalidString;
    public int SocialCredit { get; init; } = InvalidId;
    public int PictureId { get; init; } = InvalidId;
    public DateTime LastActivity { get; init; } = InvalidDate;
    public int SelectedCheckersId { get; init; } = InvalidId;
    public int SelectedAnimationId { get; init; } = InvalidId;
    public UserType Type { get; init; } = UserType.Invalid;

    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidId ||
                             Nick == InvalidString ||
                             SocialCredit == InvalidId ||
                             PictureId == InvalidId ||
                             LastActivity == InvalidDate ||
                             SelectedCheckersId == InvalidId ||
                             SelectedAnimationId == InvalidId ||
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
        PictureId = data.PictureId;
        LastActivity = data.LastActivity;
        SelectedAnimationId = data.SelectedAnimationId;
        SelectedCheckersId = data.SelectedCheckersId;
    }

    public IEnumerable<int> Achievements { get; set; } = InvalidEnumerable;

    [JsonIgnore] public override bool IsValid => base.IsValid && Achievements.Any();
}


public sealed class FriendUserData : PublicUserData
{
    public FriendUserData(PublicUserData data) : base(data)
    {
        Achievements = data.Achievements;
    }
    public new static readonly FriendUserData Invalid = new(PublicUserData.Invalid);

    public int ChatId { get; set; } = InvalidId;

    [JsonIgnore] public override bool IsValid => base.IsValid && ChatId != InvalidId;
}


public sealed class User : BasicUserData
{

    public new static readonly User Invalid = new(BasicUserData.Invalid);

    public User(BasicUserData data)
    {
        Id = data.Id;
        Nick = data.Nick;
        SocialCredit = data.SocialCredit;
        PictureId = data.PictureId;
        LastActivity = data.LastActivity;
        SelectedAnimationId = data.SelectedAnimationId;
        SelectedCheckersId = data.SelectedCheckersId;
    }

    public IEnumerable<int> Items { get; set; } = InvalidEnumerable;
    public IEnumerable<Friendship> Friends { get; set; } = Enumerable.Empty<Friendship>();
    public IEnumerable<int> Games { get; set; } = InvalidEnumerable;

    [JsonIgnore]
    public override bool IsValid => base.IsValid && Items.Any();
}