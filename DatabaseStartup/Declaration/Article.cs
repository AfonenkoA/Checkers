using static Common.Entity.UserType;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Declaration;

internal static class Article
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

    private static readonly string Create = $@"
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

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectArticleProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{ArticleTable} WHERE {Id}={IdVar};
END";

    private static readonly string SelectNews = $@"
GO
CREATE PROCEDURE {SelectNewsProc}
AS
BEGIN
    SELECT * FROM {Schema}.{ArticleTable};
END";

    private static readonly string UpdateTitle = $@"
GO
CREATE PROCEDURE {UpdateArticleTitleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleTitleVar} {UniqueStringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticleTitle} = {ArticleTitleVar} WHERE {Id}={IdVar}
END";

    private static readonly string UpdateArticle = $@"
GO
CREATE PROCEDURE {UpdateArticleAbstractProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleAbstractVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticleAbstract} = {ArticleAbstractVar} WHERE {Id}={IdVar}
END";

    private static readonly string UpdateContent = $@"
GO
CREATE PROCEDURE {UpdateArticleContentProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleContentVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticleContent} = {ArticleContentVar} WHERE {Id}={IdVar}
END";

    private static readonly string UpdatePicture = $@"
GO
CREATE PROCEDURE {UpdateArticlePictureIdProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticlePictureIdVar} INT
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticlePictureId} = {ArticlePictureIdVar} WHERE {Id}={IdVar}
END";

    private static readonly string UpdatePost = $@"
GO
CREATE PROCEDURE {UpdateArticlePostIdProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticlePostIdVar} INT
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Moderator)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticlePostId} = {ArticlePostIdVar} WHERE {Id}={IdVar}
END";

    public static readonly string Function = $@"
{Create}
{Select}
{SelectNews}
{UpdatePicture}
{UpdateArticle}
{UpdateContent}
{UpdatePost}
{UpdateTitle}";
}