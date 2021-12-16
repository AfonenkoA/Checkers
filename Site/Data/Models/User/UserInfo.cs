using Common.Entity;

namespace Site.Data.Models.User;

public class UserInfo
{
    internal int Id { get; }
    internal string Nick { get; }
    internal DateTime LastActivity { get; }
    internal string PictureUrl { get; init; } = String.Empty;
    internal string Type { get; }
    internal int SocialCredit { get; }
    public UserInfo(BasicUserData data)
    {
        Id = data.Id;
        Nick = data.Nick;
        LastActivity = data.LastActivity;
        Type = data.Type.ToString();
        SocialCredit = data.SocialCredit;
    }
}