using System;
using System.Data;
using Checkers.Data.Entity;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;
using static Checkers.Data.Repository.MSSqlImplementation.Repository;
using static Checkers.Data.Repository.MSSqlImplementation.NewsRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ForumRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ItemRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ResourceRepository;
using static Checkers.Data.Repository.MSSqlImplementation.MessageRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

internal static class SqlExtensions
{
    internal static T GetFieldValue<T>(this SqlDataReader reader, string col) => (T)reader[Unwrap(col)];

    internal static BasicUserData GetUser(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Nick = reader.GetFieldValue<string>(Nick),
            SocialCredit = reader.GetFieldValue<int>(SocialCredit),
            PictureId = reader.GetFieldValue<int>(PictureId),
            SelectedAnimationId = reader.GetFieldValue<int>(AnimationId),
            SelectedCheckersId = reader.GetFieldValue<int>(CheckersSkinId),
            LastActivity = reader.GetFieldValue<DateTime>(LastActivity),
            Type = (UserType)reader.GetFieldValue<int>(UserTypeId)
        };

    internal static ArticleInfo GetArticle(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Title = reader.GetFieldValue<string>(ArticleTitle),
            Abstract = reader.GetFieldValue<string>(ArticleAbstract),
            PictureId = reader.GetFieldValue<int>(ArticlePictureId)
        };

    internal static PostInfo GetPost(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Title = reader.GetFieldValue<string>(PostTitle),
            Content = reader.GetFieldValue<string>(PostContent),
            PictureId = reader.GetFieldValue<int>(PostPictureId)
        };

    internal static Friendship GetFriendship(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(User2Id),
            State = FriendshipState.Accepted
        };


    private static Item GetItem(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Resource = new ResourceInfo
            {
                Id = reader.GetFieldValue<int>(ResourceId),
                Extension = reader.GetFieldValue<string>(ResourceExtension)
            }
        };

    public static Message GetMessage(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            SendTime = reader.GetFieldValue<DateTime>(SendTime),
            UserId = reader.GetFieldValue<int>(UserId),
            Content = reader.GetFieldValue<string>(MessageContent)
        };

    private static NamedItem GetNamedItem(this SqlDataReader reader) =>
        new(reader.GetItem()) {Name = reader.GetFieldValue<string>(Name)};

    private static DetailedItem GetDetailedItem(this SqlDataReader reader) =>
        new(reader.GetNamedItem()) {Detail = reader.GetFieldValue<string>(Detail)};

    private static SoldItem GetSoldItem(this SqlDataReader reader) =>
        new(reader.GetDetailedItem()) {Price = reader.GetFieldValue<int>(Price)};

    public static Picture GetPicture(this SqlDataReader reader) => new(reader.GetNamedItem());

    public static Achievement GetAchievement(this SqlDataReader reader) => new(reader.GetDetailedItem());

    public static Animation GetAnimation(this SqlDataReader reader) => new(reader.GetSoldItem());

    public static CheckersSkin GetCheckersSkin(this SqlDataReader reader) => new(reader.GetSoldItem());

    public static LootBox GetLootBox(this SqlDataReader reader) => new(reader.GetSoldItem());

    internal static int GetReturn(this SqlCommand command) => (int)command.Parameters[ReturnValue].Value;

    internal static SqlParameter LoginParameter(string login) =>
            new() { ParameterName = LoginVar, SqlDbType = SqlDbType.NVarChar, Value = login };

    internal static SqlParameter PasswordParameter(string password) =>
        new() { ParameterName = PasswordVar, SqlDbType = SqlDbType.NVarChar, Value = password };

    internal static SqlParameter IdParameter(int id) =>
        new() { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = id };

}