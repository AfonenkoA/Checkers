using System.Text.Json.Serialization;
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
    public int PictureId { get; init; } = InvalidInt;
    public DateTime LastActivity { get; init; } = InvalidDate;
    public int SelectedCheckersId { get; init; } = InvalidInt;
    public int SelectedAnimationId { get; init; } = InvalidInt;
    public UserType Type { get; init; } = UserType.Invalid;

    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidInt ||
                             Nick == InvalidString ||
                             SocialCredit == InvalidInt ||
                             PictureId == InvalidInt ||
                             LastActivity == InvalidDate ||
                             SelectedCheckersId == InvalidInt ||
                             SelectedAnimationId == InvalidInt ||
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
        Type = data.Type;
    }
    [JsonConstructor]
    public PublicUserData() { }
    public IEnumerable<Achievement> Achievements { get; set; } = Empty<Achievement>();

    [JsonIgnore] public override bool IsValid => base.IsValid;
}


public sealed class FriendUserData : PublicUserData
{
    public FriendUserData(PublicUserData data) : base(data)
    {
        Achievements = data.Achievements;
    }
    [JsonConstructor]
    public FriendUserData() { }
    public new static readonly FriendUserData Invalid = new(PublicUserData.Invalid);

    public int ChatId { get; set; } = InvalidInt;

    [JsonIgnore] public override bool IsValid => base.IsValid && ChatId != InvalidInt;
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
        Type = data.Type;
    }

    [JsonConstructor]
    public User() { }

    public IEnumerable<CheckersSkin> CheckerSkins { get; set; } = Empty<CheckersSkin>();
    public IEnumerable<Animation> Animations { get; set; } = Empty<Animation>();
    public IEnumerable<Emotion> Emotions { get; set; } = Empty<Emotion>();
    public IEnumerable<Friendship> Friends { get; set; } = Empty<Friendship>();

    public IEnumerable<Animation> AvailableAnimations { get; set; } = Empty<Animation>();
    public IEnumerable<CheckersSkin> AvailableCheckersSkins { get; set; } = Empty<CheckersSkin>();
    public IEnumerable<LootBox> AvailableLootBox { get; set; } = Empty<LootBox>();

    [JsonIgnore]
    public override bool IsValid => base.IsValid &&
                                    CheckerSkins.Any() &&
                                    Animations.Any();
}