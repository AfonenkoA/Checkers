﻿using Common.Entity;

namespace Site.Data.Models.User;

public class UserInfo
{
    internal int Id { get; }
    internal string Nick { get; }
    internal DateTime LastActivity { get; }
    internal string Type { get; }
    internal int SocialCredit { get; }
    internal PictureView Picture { get; }
    public UserInfo(BasicUserData data, PictureView picture)
    {
        Picture = picture;
        Id = data.Id;
        Nick = data.Nick;
        LastActivity = data.LastActivity;
        Type = data.Type.ToString();
        SocialCredit = data.SocialCredit;
    }

    public UserInfo(UserInfo info, PictureView picture)
    {
        Picture = picture;
        Id = info.Id;
        Nick = info.Nick;
        Type = info.Type;
        LastActivity = info.LastActivity;
        SocialCredit = info.SocialCredit;
    }
}