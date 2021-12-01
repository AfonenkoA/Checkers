using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class NewsRepository: Repository, INewsRepository
{
    public const string ArticleTable = "[Article]";
    public const string ArticleAuthorId = "[article_author_id]";
    public const string ArticleTitle = "[article_title]";
    public const string ArticleAbstract = "[article_abstract]";
    public const string ArticleContent = "[article_content]";
    public const string ArticleCreated = "[article_created]";
    public const string ArticlePictureId = "[article_picture_id]";
    public const string ArticlePostId = "[article_post_id]";

    public const string CreateArticleProc = "[SP_CreateArticle]";
    public const string SelectArticleProc = "[SP_SelectArticleProc]";
    public const string SelectNewsProc = "[SP_SelectNews]";
    public const string UpdateArticleTitleProc = "[SP_UpdateArticleTitle]";
    public const string UpdateArticleContentProc = "[SP_UpdateArticleContent]";
    public const string UpdateArticleAbstractProc = "[SP_UpdateArticleAbstract]";
    public const string UpdateArticlePictureIdProc = "[SP_UpdateArticlePictureId]";
    public const string UpdateArticlePostIdProc = "[SP_UpdateArticlePostId]";

    public const string ArticleTitleVar = "@article_title";
    public const string ArticleAbstractVar = "@abstract";
    public const string ArticlePictureIdVar = "@article_picture_id";
    public const string ArticleContentVar = "@article_content";
    public const string ArticlePostIdVar = "@article_post_id";

    internal NewsRepository(SqlConnection connection) : base(connection) { }

    public bool CreateArticle(Credential credential, ArticleCreationData article)
    {
        using var command = CreateProcedure(CreateArticleProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                new SqlParameter{ParameterName = ArticleTitleVar,SqlDbType = SqlDbType.NVarChar,Value = article.Title},
                new SqlParameter{ParameterName = ArticleAbstractVar,SqlDbType = SqlDbType.NVarChar,Value = article.Abstract},
                new SqlParameter{ParameterName = ArticleContentVar,SqlDbType = SqlDbType.NVarChar,Value = article.Content},
                new SqlParameter{ParameterName = ArticlePictureIdVar,SqlDbType = SqlDbType.Int,Value = article.PictureId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateTitle(Credential credential, int id, string title)
    {
        using var command = CreateProcedure(UpdateArticleTitleProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(id),
                new SqlParameter{ParameterName = ArticleTitleVar,SqlDbType = SqlDbType.NVarChar,Value = title}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateAbstract(Credential credential, int id, string @abstract)
    {
        using var command = CreateProcedure(UpdateArticleAbstractProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(id),
                new SqlParameter{ParameterName = ArticleAbstractVar,SqlDbType = SqlDbType.NVarChar,Value = @abstract}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateContent(Credential credential, int id, string content)
    {
        using var command = CreateProcedure(UpdateArticleContentProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(id),
                new SqlParameter{ParameterName = ArticleContentVar,SqlDbType = SqlDbType.NVarChar,Value = content}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdatePictureId(Credential credential, int id, int pictureId)
    {
        using var command = CreateProcedure(UpdateArticlePictureIdProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(id),
                new SqlParameter{ParameterName = ArticlePictureIdVar,SqlDbType = SqlDbType.Int,Value = pictureId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdatePostId(Credential credential, int id, int postId)
    {
        using var command = CreateProcedure(UpdateArticlePostIdProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(id),
                new SqlParameter{ParameterName = ArticlePostIdVar,SqlDbType = SqlDbType.Int,Value = postId}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool DeleteArticle(Credential credential, int articleId)
    {
        throw new NotImplementedException();
    }

    public Article GetArticle(int articleId)
    {
        using var command = CreateProcedure(SelectArticleProc);
        command.Parameters.Add(new SqlParameter { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = articleId });
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return Article.Invalid;
        return new Article(reader.GetArticle())
        {
            PostId = reader.GetFieldValue<int>(ArticlePostId),
            Content = reader.GetFieldValue<string>(ArticleContent),
            Created = reader.GetFieldValue<DateTime>(ArticleCreated)
        };
    }

    public IEnumerable<ArticleInfo> GetNews()
    {
        var list = new List<ArticleInfo>();
        using var command = CreateProcedure(SelectNewsProc);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            list.Add(reader.GetArticle());
        return list;
    }
    
}