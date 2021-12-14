using static Common.Entity.ChatType;
using static DatabaseStartup.Declaration.Markup;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;

namespace DatabaseStartup.Declaration;

internal static class Message
{
    internal static readonly string Table = $@"
CREATE TABLE {MessageTable}
(
{Identity},
{ChatId}            INT             NOT NULL	{Fk(MessageTable, ChatTable)},
{UserId}            INT             NOT NULL	{Fk(MessageTable, UserTable)}, 
{MessageContent}    {StringType}    NOT NULL,
{SendTime}          DATETIME        NOT NULL    DEFAULT GETDATE()
);";

    private static readonly string Send = $@"
GO
CREATE PROCEDURE {SendMessageProc} {LoginVar} {UniqueStringType},{PasswordVar} {StringType},
{ChatIdVar} INT,{MessageContentVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT, {IdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        EXEC {IdVar} = {GetChatTypeByNameProc} {SqlString(Public)}
        IF {ChatIdVar} IN 
        (SELECT {ChatId} FROM {FriendshipTable} WHERE {User1Id}={UserIdVar}) OR 
        (SELECT {ChatTypeId} FROM {ChatTable} WHERE {Id}={ChatIdVar}) = {IdVar}
            INSERT INTO {Schema}.{MessageTable}({ChatId},{UserId},{MessageContent})
            VALUES ({ChatIdVar},{UserIdVar},{MessageContentVar}); 
        END
END";

    private static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectMessageProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {ChatIdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar};
    IF {UserIdVar}!={InvalidId}
            SELECT * FROM {Schema}.{MessageTable} WHERE {ChatId}={ChatIdVar};
END";

    public static readonly string Function = $@"
{Select}
{Send}";
}