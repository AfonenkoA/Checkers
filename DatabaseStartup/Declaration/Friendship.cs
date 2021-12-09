using static Common.Entity.ChatType;
using static Common.Entity.FriendshipState;
using static DatabaseStartup.CsvTable;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ChatRepository;

namespace DatabaseStartup.Declaration;

internal class Friendship
{
    internal static readonly string State = $@"
CREATE TABLE {FriendshipStateTable}
(
{Identity},
{FriendshipStateName} {UniqueStringType}  NOT NULL UNIQUE
);";

    internal static readonly string Table = $@"
CREATE TABLE {FriendshipTable}
(
{Identity},
{User1Id}           INT   NOT NULL	{Fk(FriendshipTable, UserTable, "1")},
{User2Id}           INT   NOT NULL	{Fk(FriendshipTable, UserTable, "2")},
{ChatId}            INT   NOT NULL	{Fk(FriendshipTable, ChatTable)},
{FriendshipStateId} INT   NOT NULL	{Fk(FriendshipTable, FriendshipStateTable)}
);";

    internal static readonly string StateByName = $@"
GO
CREATE PROCEDURE {GetFriendshipStateByNameProc} {FriendshipStateNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{FriendshipStateTable} WHERE {FriendshipStateName}={FriendshipStateNameVar})
END";

    internal static readonly string Create = $@"
GO
CREATE PROCEDURE {CreateFriendshipProc} {User1IdVar} INT,{User2IdVar} INT
AS
BEGIN
    DECLARE {ChatIdVar} INT, @u1_login {UniqueStringType}, @u2_login {UniqueStringType},
    {ChatNameVar} {UniqueStringType}, {IdVar} INT
    BEGIN TRANSACTION;  
    SET @u1_login = (SELECT {Login} FROM {Schema}.{UserTable} WHERE {Id}={User1IdVar});
    SET @u2_login = (SELECT {Login} FROM {Schema}.{UserTable} WHERE {Id}={User2IdVar});
    SET {ChatNameVar} = N'Chat '+@u1_login+N' to '+ @u2_login;
    EXEC {ChatIdVar} = {CreateChatProc} {ChatNameVar}, {SqlString(Private)};
    EXEC {IdVar} = {GetFriendshipStateByNameProc} {SqlString(Accepted)};
    INSERT INTO {Schema}.{FriendshipTable}({User1Id},{User2Id},{ChatId},{FriendshipStateId})
    VALUES ({User1IdVar},{User2IdVar},{ChatIdVar},{IdVar}),({User2IdVar},{User1IdVar},{ChatIdVar},{IdVar});
    COMMIT;
END";
}