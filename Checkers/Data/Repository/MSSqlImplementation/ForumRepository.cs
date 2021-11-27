using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;
using static Checkers.Data.Repository.MSSqlImplementation.ChatRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class ForumRepository : Repository, IForumRepository
{
    public const string PostTable = "[Post]";

    public const string PostTitle = "[post_title]";
    public const string PostAuthorId = "[post_author_id]";
    public const string PostContent = "[post_content]";
    public const string PostCreated = "[post_created]";
    public const string PostPictureId = "[post_picture_id]";

    public const string PostTitleVar = "@post_title";
    public const string PostContentVar = "@post_content";
    public const string PostPictureIdVar = "@post_picture_id";
    public const string PostAuthorIdVar = "@post_author_id";

    public const string CreatePostProc = "[SP_CreatePost]";
    public const string SelectPostInfoProc = "[SP_SelectPostInfo]";
    public const string SelectPostProc = "[SP_SelectPost]";
    public const string SelectPostsProc = "[SP_SelectPosts]";
    public const string UpdatePostTitleProc = "[SP_UpdatePostTitle]";
    public const string UpdatePostContentProc = "[SP_UpdatePostContent]";
    public const string UpdatePostPictureIdProc = "[SP_UpdatePostPictureId]";

    internal ForumRepository(SqlConnection connection) : base(connection) { }

    public bool CreatePost(Credential credential, PostCreationData post)
    {
        using var command = CreateProcedure(CreatePostProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                new SqlParameter{ParameterName = PostTitle,SqlDbType = SqlDbType.NVarChar,Value = post.Title},
                new SqlParameter{ParameterName = PostContent,SqlDbType = SqlDbType.NVarChar,Value = post.Content},
                new SqlParameter{ParameterName = PostPictureId,SqlDbType = SqlDbType.Int,Value = post.PictureId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateTitle(Credential credential, int postId, string title)
    {
        using var command = CreateProcedure(UpdatePostTitleProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(postId),
                new SqlParameter{ParameterName = PostTitleVar,SqlDbType = SqlDbType.NVarChar,Value = title}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateContent(Credential credential, int postId, string content)
    {
        using var command = CreateProcedure(UpdatePostContentProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(postId),
                new SqlParameter{ParameterName = PostContentVar,SqlDbType = SqlDbType.NVarChar,Value = content}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdatePictureId(Credential credential, int postId, int imageId)
    {
        using var command = CreateProcedure(UpdatePostPictureIdProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(postId),
                new SqlParameter{ParameterName = PostPictureIdVar,SqlDbType = SqlDbType.NVarChar,Value = imageId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool DeletePost(Credential credential, int postId)
    {
        throw new NotImplementedException();
    }

    public Post GetPost(int postId)
    {
        using var command = CreateProcedure(SelectPostProc);
        command.Parameters.Add(new SqlParameter { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = postId });
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return Post.Invalid;
        return new Post(reader.GetPost())
        {
            ChatId = reader.GetFieldValue<int>(ChatId),
            AuthorId = reader.GetFieldValue<int>(PostAuthorId),
            Created = reader.GetFieldValue<DateTime>(PostCreated),
        };
    }

    public IEnumerable<PostInfo> GetPosts()
    {
        using var command = CreateProcedure(SelectPostsProc);
        using var reader = command.ExecuteReader();
        List<PostInfo> list = new();
        while (reader.Read())
            list.Add(reader.GetPost());
        return list;
    }
}
