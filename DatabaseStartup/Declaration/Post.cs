using static Common.Entity.ChatType;
using static DatabaseStartup.CsvTable;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration;

internal class Post
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

    internal static readonly string Create = $@"
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
}