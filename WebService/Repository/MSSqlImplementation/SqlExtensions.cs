using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common.Entity;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.StatisticsRepository;

namespace WebService.Repository.MSSqlImplementation;

internal static class SqlExtensions
{
    internal static T GetFieldValue<T>(this SqlDataReader reader, string col) => (T)reader[Unwrap(col)];

    public static BasicUserData GetUser(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Nick = reader.GetFieldValue<string>(Nick),
            SocialCredit = reader.GetFieldValue<int>(SocialCredit),
            LastActivity = reader.GetFieldValue<DateTime>(LastActivity),
            Type = (UserType)reader.GetFieldValue<int>(UserTypeId)
        };


    private static ArticleInfo GetArticleInfo(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Title = reader.GetFieldValue<string>(ArticleTitle),
            Abstract = reader.GetFieldValue<string>(ArticleAbstract),
            PictureId = reader.GetFieldValue<int>(ArticlePictureId)
        };

    public static Article GetArticle(this SqlDataReader reader) =>
        new(reader.GetArticleInfo())
        {
            PostId = reader.GetFieldValue<int>(ArticlePostId),
            Content = reader.GetFieldValue<string>(ArticleContent),
            Created = reader.GetFieldValue<DateTime>(ArticleCreated)
        };

    public static IEnumerable<ArticleInfo> GetAllArticleInfo(this SqlDataReader reader)
    {
        var list = new List<ArticleInfo>();
        while (reader.Read())
            list.Add(reader.GetArticle());
        return list;
    }

    private static PostInfo GetPostInfo(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Title = reader.GetFieldValue<string>(PostTitle),
            Content = reader.GetFieldValue<string>(PostContent),
            PictureId = reader.GetFieldValue<int>(PostPictureId)
        };

    public static Post GetPost(this SqlDataReader reader) =>
        new(reader.GetPostInfo())
        {
            ChatId = reader.GetFieldValue<int>(ChatId),
            AuthorId = reader.GetFieldValue<int>(PostAuthorId),
            Created = reader.GetFieldValue<DateTime>(PostCreated),
        };

    public static IEnumerable<PostInfo> GetAllPostInfo(this SqlDataReader reader)
    {
        List<PostInfo> list = new();
        while (reader.Read())
            list.Add(reader.GetPostInfo());
        return list;
    }

    private static Friendship GetFriendship(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(User2Id),
            State = FriendshipState.Accepted
        };

    public static IEnumerable<Friendship> GetAllFriendship(this SqlDataReader reader)
    {
        var list = new List<Friendship>();
        while (reader.Read())
            list.Add(reader.GetFriendship());
        return list;
    }


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

    private static Message GetMessage(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            SendTime = reader.GetFieldValue<DateTime>(SendTime),
            UserId = reader.GetFieldValue<int>(UserId),
            Content = reader.GetFieldValue<string>(MessageContent)
        };

    public static IEnumerable<Message> GetAllMessage(this SqlDataReader reader)
    {
        List<Message> list = new();
        while (reader.Read())
            list.Add(reader.GetMessage());
        return list;
    }

    private static NamedItem GetNamedItem(this SqlDataReader reader) =>
        new(reader.GetItem()) { Name = reader.GetFieldValue<string>(ItemName) };

    private static DetailedItem GetDetailedItem(this SqlDataReader reader) =>
        new(reader.GetNamedItem()) { Detail = reader.GetFieldValue<string>(Detail) };

    private static SoldItem GetSoldItem(this SqlDataReader reader) =>
        new(reader.GetDetailedItem()) { Price = reader.GetFieldValue<int>(Price) };

    public static Picture GetPicture(this SqlDataReader reader) => new(reader.GetNamedItem());

    public static IEnumerable<Picture> GetAllPicture(this SqlDataReader reader)
    {
        List<Picture> list = new();
        while (reader.Read())
            list.Add(reader.GetPicture());
        return list;
    }

    public static Achievement GetAchievement(this SqlDataReader reader) => new(reader.GetDetailedItem());

    public static IEnumerable<Achievement> GetAllAchievement(this SqlDataReader reader)
    {
        List<Achievement> list = new();
        while (reader.Read())
            list.Add(reader.GetAchievement());
        return list;
    }

    public static Animation GetAnimation(this SqlDataReader reader) => new(reader.GetSoldItem());

    public static IEnumerable<Animation> GetAllAnimation(this SqlDataReader reader)
    {
        List<Animation> list = new();
        while (reader.Read())
            list.Add(reader.GetAnimation());
        return list;
    }

    public static CheckersSkin GetCheckersSkin(this SqlDataReader reader) => new(reader.GetSoldItem());

    public static IEnumerable<CheckersSkin> GetAllCheckersSkin(this SqlDataReader reader)
    {
        List<CheckersSkin> list = new();
        while (reader.Read())
            list.Add(reader.GetCheckersSkin());
        return list;
    }

    public static LootBox GetLootBox(this SqlDataReader reader) => new(reader.GetSoldItem());

    public static IEnumerable<LootBox> GetAlLootBox(this SqlDataReader reader)
    {
        List<LootBox> list = new();
        while (reader.Read())
            list.Add(reader.GetLootBox());
        return list;
    }

    public static IDictionary<long, int> GetTopUsers(this SqlDataReader reader)
    {
        var dict = new Dictionary<long, int>();
        while (reader.Read())
            dict.Add(reader.GetFieldValue<long>(StatisticPosition), reader.GetFieldValue<int>(Id));
        return dict;
    }

    public static Emotion GetEmotion(this SqlDataReader reader) => new(reader.GetNamedItem());

    public static IEnumerable<Emotion> GetAllEmotion(this SqlDataReader reader)
    {
        List<Emotion> list = new();
        while (reader.Read())
            list.Add(reader.GetEmotion());
        return list;
    }

    public static (byte[], string) GetFile(this SqlDataReader reader) =>
        (reader.GetFieldValue<byte[]>(ResourceBytes),
            reader.GetFieldValue<string>(ResourceExtension));

    internal static int GetReturn(this SqlCommand command) => (int)command.Parameters[ReturnValue].Value;

    internal static SqlParameter LoginParameter(string login) =>
            new() { ParameterName = LoginVar, SqlDbType = SqlDbType.NVarChar, Value = login };

    internal static SqlParameter PasswordParameter(string password) =>
        new() { ParameterName = PasswordVar, SqlDbType = SqlDbType.NVarChar, Value = password };

    internal static SqlParameter IdParameter(int id) =>
        new() { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = id };

}