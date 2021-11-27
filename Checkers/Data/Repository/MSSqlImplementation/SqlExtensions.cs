using System.Data;
using Checkers.Data.Entity;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;
using static Checkers.Data.Repository.MSSqlImplementation.Repository;
using static Checkers.Data.Repository.MSSqlImplementation.NewsRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ForumRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

internal static class SqlExtensions
{
    internal static int GetReturn(this SqlCommand command) => (int)command.Parameters["@RETURN_VALUE"].Value;
    internal static T GetFieldValue<T>(this SqlDataReader reader, string col) => (T)reader[Unwrap(col)];

    internal static BasicUserData GetUser(this SqlDataReader reader) =>
        new()
        {
            Id = reader.GetFieldValue<int>(Id),
            Nick = reader.GetFieldValue<string>(Nick),
            SocialCredit = reader.GetFieldValue<int>(SocialCredit),
            PictureId = reader.GetFieldValue<int>(PictureId),
            SelectedAnimationId = reader.GetFieldValue<int>(AnimationId),
            SelectedCheckersId = reader.GetFieldValue<int>(CheckersId),
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

internal static SqlParameter LoginParameter(string login) =>
        new() { ParameterName = LoginVar, SqlDbType = SqlDbType.NVarChar, Value = login };

    internal static SqlParameter PasswordParameter(string password) =>
        new() { ParameterName = LoginVar, SqlDbType = SqlDbType.NVarChar, Value = password};

    internal static SqlParameter IdParameter(int id) => 
        new() {ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = id};



}