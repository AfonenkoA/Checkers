using static Common.Entity.ChatType;
using static Common.Entity.UserType;
using static DatabaseStartup.Declaration.Markup;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration;

internal static class Post
{
    internal static readonly string Table = $@"
CREATE TABLE {PostTable}
(
{Identity},
{ChatId}            INT                 NOT NULL	{Fk(PostTable, ChatTable)},
{PostAuthorId}      INT                 NOT NULL	{Fk(PostTable, UserTable)},
{PostTitle}         {UniqueStringType}  NOT NULL    UNIQUE,
{PostContent}       {StringType}        NOT NULL,
{PostCreated}       DATETIME            NOT NULL    DEFAULT GETDATE(),
{PostPictureId}     INT                 NOT NULL    {Fk(PostTable, ResourceTable)}
);";

    private static readonly string Create = $@"
GO
CREATE PROCEDURE {CreatePostProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{PostTitleVar} {UniqueStringType}, {PostContentVar} {StringType},{PostPictureIdVar} INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE {IdVar} INT, {UserIdVar} INT, {ChatNameVar} {UniqueStringType}
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        SET {ChatNameVar} = N'Chat ' + {PostTitleVar};
        EXEC {IdVar} = {CreateChatProc} {ChatNameVar}, {SqlString(Public)};
        IF {IdVar} IS NOT NULL
            BEGIN
            INSERT INTO {Schema}.{PostTable}({PostContent},{PostTitle},{PostPictureId},{ChatId},{PostAuthorId})
            VALUES ({PostContentVar},{PostTitleVar},{PostPictureIdVar},{IdVar},{UserIdVar});
            COMMIT;
            RETURN @@IDENTITY;
            END
        ROLLBACK;
        END
    ELSE
        BEGIN
        ROLLBACK;
        RETURN {InvalidId};
        END
END";

    private const string SelectInfo = $@"
GO
CREATE PROCEDURE {SelectPostInfoProc} {IdVar} INT
AS
BEGIN
    SELECT {Id},{PostTitle},{PostPictureId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar};
END";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectPostProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{PostTable} WHERE {Id}={IdVar};
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectPostsProc}
AS
BEGIN
    SELECT {Id},{PostTitle},{PostPictureId},{PostContent} FROM {Schema}.{PostTable};
END";

    private static readonly string UpdateTitle = $@"
GO
CREATE PROCEDURE {UpdatePostTitleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {PostTitleVar} {UniqueStringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Moderator)}
    IF {PostAuthorIdVar}={UserIdVar} OR {AccessVar}={ValidAccess}
        UPDATE {Schema}.{PostTable} SET {PostTitle}={PostTitleVar} WHERE {Id}={IdVar}
END";

    private static readonly string UpdateContent = $@"
GO
CREATE PROCEDURE {UpdatePostContentProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {PostContentVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Moderator)}
    IF {PostAuthorIdVar}={UserIdVar} OR {AccessVar}={ValidAccess}
        UPDATE {Schema}.{PostTable} SET {PostContent}={PostContentVar} WHERE {Id}={IdVar}
END";

    private static readonly string UpdatePicture = $@"
GO
CREATE PROCEDURE {UpdatePostPictureIdProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, 
{IdVar} INT, {PostPictureIdVar} INT
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(Moderator)}
    IF {PostAuthorIdVar}={UserIdVar} OR {AccessVar}={ValidAccess}
        UPDATE {Schema}.{PostTable} SET {PostPictureId}={PostPictureIdVar} WHERE {Id}={IdVar}
END";

    public static readonly string Function = $@"
--Chat
{Create}
{Select}
{SelectInfo}
{SelectAll}
{UpdatePicture}
{UpdateContent}
{UpdateTitle}";
}