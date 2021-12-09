using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Declaration.UserItem;

internal class UserCheckersSkin
{
    public static readonly string Table = $@"
CREATE TABLE {UserCheckersSkinTable}
(
{Identity},
{CheckersSkinId}   INT     NOT NULL    {Fk(UserCheckersSkinTable, CheckersSkinTable)},
{UserId}        INT     NOT NULL    {Fk(UserCheckersSkinTable, UserTable)}
);";

    public static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectUserCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT C.* FROM {Schema}.{UserCheckersSkinTable} AS UC
    JOIN {CheckersSkinTable} AS C ON UC.{CheckersSkinId}=C.{Id}
    WHERE {Id}={IdVar}
END";

    public static readonly string Update = $@"
GO
CREATE PROCEDURE {UpdateUserCheckersProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {CheckersSkinId} FROM {Schema}.{UserCheckersSkinTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {CheckersSkinId}={IdVar} WHERE {UserAuthCondition};
END";
}