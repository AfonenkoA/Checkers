using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.Declaration.Markup;


namespace DatabaseStartup.Declaration;

internal static class Chat
{
    public static readonly string Type = $@"
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

    public static readonly string TypeByName = $@"
GO
CREATE PROCEDURE {GetChatTypeByNameProc} {ChatTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{ChatTypeTable} WHERE {ChatTypeName}={ChatTypeNameVar})
END";

    public static readonly string Create = $@"
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

    public static readonly string GetCommon = $@"
GO
CREATE PROCEDURE {GetCommonChatIdProc}  {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {ChatTable} WHERE {ChatName}={SqlString(CommonChatName)});
END";
}