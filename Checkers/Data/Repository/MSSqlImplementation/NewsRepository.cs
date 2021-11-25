using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class NewsRepository: MessageRepository, INewsRepository
{
    public const string ArticleTable = "[Article]";
    public const string ArticleAuthorId = "[article_author_id]";
    public const string ArticleTitle = "[article_title]";
    public const string ArticleAbstract = "[abstract]";
    public const string ArticleContent = "[article_content]";
    public const string ArticleCreated = "[article_created]";
    public const string ArticlePictureId = "[article_picture_id]";
    public const string ArticlePostId = "[article_post_id]";
    public const string ArticleId = "[article_id]";

    public const string CreateArticleProc = "[SP_CreateArticle]";
    public const string SelectArticleInfoProc = "[SP_SelectArticleInfo]";
    public const string SelectArticleProc = "[SP_SelectArticleProc]";
    public const string SelectNewsProc = "[SP_SelectNews]";

    public const string ArticleTitleVar = "@article_title";
    public const string ArticleAbstractVar = "@abstract";
    public const string ArticlePictureIdVar = "@article_picture_id";
    public const string ArticleContentVar = "@article_content";


    public bool CreateArticle(Credential credential, ArticleCreationData article)
    {
        using var command = new SqlCommand(CreateArticleProc, Connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddRange(
            new[]
            {
                new SqlParameter{ParameterName = LoginVar, SqlDbType = SqlDbType.NVarChar, Value = credential.Login},
                new SqlParameter{ParameterName = PasswordVar,SqlDbType = SqlDbType.NVarChar,Value = credential.Password},
                new SqlParameter{ParameterName = ArticleTitleVar,SqlDbType = SqlDbType.NVarChar,Value = article.Title},
                new SqlParameter{ParameterName = ArticleAbstract,SqlDbType = SqlDbType.NVarChar,Value = article.Abstract},
                new SqlParameter{ParameterName = ArticleContent,SqlDbType = SqlDbType.NVarChar,Value = article.Content},
                new SqlParameter{ParameterName = ArticlePictureId,SqlDbType = SqlDbType.Int,Value = article.PictureId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateTitle(Credential credential, int id, string title)
    {
        throw new NotImplementedException();
    }

    public bool UpdateAbstract(Credential credential, int id, string @abstract)
    {
        throw new NotImplementedException();
    }

    public bool UpdateContent(Credential credential, int id, string content)
    {
        throw new NotImplementedException();
    }

    public bool UpdatePicture(Credential credential, int id, int pictureId)
    {
        throw new NotImplementedException();
    }

    public bool UpdatePost(Credential credential, int id, int postId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteArticle(Credential credential, int articleId)
    {
        throw new NotImplementedException();
    }

    public Article GetArticle(int articleId)
    {
        var article = new Article();
        using var command = new SqlCommand(SelectArticleProc, Connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.Add(new SqlParameter { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = articleId });
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return article;
        article.Id = reader.GetFieldValue<int>(Id);
        article.Title = reader.GetFieldValue<string>(ArticleTitle);
        article.Abstract = reader.GetFieldValue<string>(ArticleAbstract);
        article.PictureId = reader.GetFieldValue<int>(ArticlePictureId);
        article.Updated = reader.GetFieldValue<DateTime>(ArticleCreated);
        article.PostId = reader.GetFieldValue<int>(ArticlePostId);
        return article;
    }

    public IEnumerable<ArticleInfo> GetNews()
    {
        var list = new List<ArticleInfo>();
        using var command = new SqlCommand(SelectNewsProc, Connection) { CommandType = CommandType.StoredProcedure };
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return list;
        list.Add(new ArticleInfo
        {
            Id = reader.GetFieldValue<int>(Id),
            Title = reader.GetFieldValue<string>(ArticleTitle),
            Abstract = reader.GetFieldValue<string>(ArticleAbstract),
            PictureId = reader.GetFieldValue<int>(ArticlePictureId)
        });
        return list;
    }
    
}