using static Common.Entity.UserType;
using static DatabaseStartup.CsvTable;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration;

internal class Article
{
    internal static readonly string Table = $@"
CREATE TABLE {ArticleTable}
(
{Identity},
{ArticleAuthorId}   INT                 NOT NULL	{Fk(ArticleTable, UserTable)},
{ArticleTitle}      {UniqueStringType}  NOT NULL    UNIQUE,
{ArticleAbstract}   {StringType}        NOT NULL,
{ArticleContent}    {StringType}        NOT NULL,
{ArticleCreated}    DATETIME            NOT NULL    DEFAULT GETDATE(),
{ArticlePictureId}  INT                 NOT NULL    {Fk(ArticleTable, ResourceTable)},
{ArticlePostId}     INT                 NOT NULL    {Fk(ArticleTable, PostTable)},
);";

    internal static readonly string Create = $@"
GO
CREATE PROCEDURE {CreateArticleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{ArticleTitleVar} {UniqueStringType}, {ArticleAbstractVar} {StringType}, 
{ArticleContentVar} {StringType}, {ArticlePictureIdVar} INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE {IdVar} INT, {UserIdVar} INT, @post {StringType}, {AccessVar} INT
    SET @post = N'Discussion ' + {ArticleTitleVar};
    EXEC {UserIdVar}={AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Editor)};
        IF {AccessVar}={ValidAccess}
            BEGIN
            EXEC {IdVar}={CreatePostProc} {LoginVar},{PasswordVar},{ArticleTitleVar},@post, {ArticlePictureIdVar};
            IF {IdVar}!={InvalidId}
                BEGIN
                INSERT INTO {Schema}.{ArticleTable}({ArticleTitle},{ArticleContent},{ArticleAbstract},{ArticleAuthorId},{ArticlePictureId},{ArticlePostId})
                VALUES  ({ArticleTitleVar},{ArticleContentVar},{ArticleAbstractVar},{UserIdVar},{ArticlePictureIdVar},{IdVar});
                COMMIT;
                RETURN @@IDENTITY
                END
            END
        ROLLBACK;
        END
    ELSE
        BEGIN
        ROLLBACK;
        RETURN {InvalidId};
        END
    
END";
}