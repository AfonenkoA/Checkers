using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

public class ForumRepository : MessageRepository, IForumRepository
{
    public const string PostTable = "[Post]";

    public const string PostId = "[post_id]";
    public const string PostTitle = "[post_title]";
    public const string PostAuthorId = "[post_author_id]";
    public const string PostContent = "[post_content]";
    public const string PostCreated = "[post_created]";
    public const string PostPictureId = "[post_picture_id]";

    public const string PostTitleVar = "@post_title";
    public const string PostContentVar = "@post_content";
    public const string PostPictureIdVar = "@post_picture_id";

    public const string CreatePostProc = "[SP_CreatePost]";
    public const string SelectPostInfoProc = "[SP_SelectPostInfo]";
    public const string SelectPostProc = "[SP_SelectPost]";
    public const string SelectPostsProc = "[SP_SelectPosts]";

    public bool CreatePost(Credential credential, PostCreationData post)
    {
        using var command = new SqlCommand(CreatePostProc, Connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddRange(
            new[]
            {
                new SqlParameter{ParameterName = LoginVar, SqlDbType = SqlDbType.NVarChar, Value = credential.Login},
                new SqlParameter{ParameterName = PasswordVar,SqlDbType = SqlDbType.NVarChar,Value = credential.Password},
                new SqlParameter{ParameterName = PostTitle,SqlDbType = SqlDbType.NVarChar,Value = post.Title},
                new SqlParameter{ParameterName = PostContent,SqlDbType = SqlDbType.NVarChar,Value = post.Content},
                new SqlParameter{ParameterName = PostPictureId,SqlDbType = SqlDbType.Int,Value = post.PictureId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateTitle(Credential credential, int postId, string title)
    {
        throw new NotImplementedException();
    }

    public bool UpdateContent(Credential credential, int postId, string content)
    {
        throw new NotImplementedException();
    }

    public bool UpdatePicture(Credential credential, int postId, int imageId)
    {
        throw new NotImplementedException();
    }

    public bool DeletePost(Credential credential, int postId)
    {
        throw new NotImplementedException();
    }

    public Post GetPost(int postId)
    {
        var post = new Post();
        using var command = new SqlCommand(SelectPostProc, Connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.Add(new SqlParameter { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = postId });
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return post;
        post.Id = reader.GetFieldValue<int>(Id);
        post.Title = reader.GetFieldValue<string>(PostTitle);
        post.PictureId = reader.GetFieldValue<int>(PostPictureId);
        post.Created = reader.GetFieldValue<DateTime>(PostCreated);
        post.Content = reader.GetFieldValue<string>(PostContent);
        return post;
    }

    public bool CommentPost(Credential credential, int postId, string comment)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PostInfo> GetPosts()
    {
        using var command =  new SqlCommand(SelectPostsProc, Connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        using var reader = command.ExecuteReader();
        List<PostInfo> list = new();
        while (reader.Read())
            list.Add(new PostInfo
            {
                Id = reader.GetFieldValue<int>(Id),
                Title = reader.GetFieldValue<string>(PostTitle),
                Content = reader.GetFieldValue<string>(PostContent),
                PictureId = reader.GetFieldValue<int>(PostPictureId)
            });
        return list;
    }
}
