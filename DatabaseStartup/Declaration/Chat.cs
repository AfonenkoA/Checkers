using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static DatabaseStartup.Declaration.Markup;


namespace DatabaseStartup.Declaration;

internal static class Chat
{
    public const string Type = $@"
CREATE TABLE {ChatTypeTable}
(
{Identity},
{ChatTypeName}      {UniqueStringType}    NOT NULL UNIQUE
);";

    public static readonly string Table = $@"
CREATE TABLE {ChatTable}
(
{Identity},
{ChatName}      {UniqueStringType}  NOT NULL  UNIQUE,
{ChatTypeId}    INT                 NOT NULL {Fk(ChatTable, ChatTypeTable)}
);";

    private const string TypeByName = $@"
GO
CREATE PROCEDURE {GetChatTypeByNameProc} {ChatTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{ChatTypeTable} WHERE {ChatTypeName}={ChatTypeNameVar})
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateChatProc} {ChatNameVar} {UniqueStringType}, {ChatTypeNameVar} {UniqueStringType}
AS
BEGIN
    DECLARE {IdVar} INT
    BEGIN TRANSACTION;  
    EXEC {IdVar} = {GetChatTypeByNameProc} {ChatTypeNameVar};
    INSERT INTO {Schema}.{ChatTable}({ChatName},{ChatTypeId}) 
    VALUES ({ChatNameVar},{IdVar});
    COMMIT;
    RETURN @@IDENTITY
END";

    private static readonly string GetCommon = $@"
GO
CREATE PROCEDURE {GetCommonChatIdProc}
AS
BEGIN
    RETURN (SELECT {Id} FROM {ChatTable} WHERE {ChatName}={SqlString(CommonChatName)});
END";

    public static readonly string Function = $@"
{TypeByName}
{GetCommon}
{Create}";
}