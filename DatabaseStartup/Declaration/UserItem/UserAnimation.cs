using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Declaration.UserItem;

internal class UserAnimation
{
    public static readonly string Table = $@"
CREATE TABLE {UserAnimationTable}
(
{Identity},
{AnimationId}   INT     NOT NULL    {Fk(UserAnimationTable, AnimationTable)},
{UserId}        INT     NOT NULL    {Fk(UserAnimationTable, UserTable)}
);";

    public static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectUserAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT A.* FROM {Schema}.{UserAnimationTable} AS UA
    JOIN {AnimationTable} AS A ON UA.{AnimationId}=A.{Id} 
    WHERE {Id}={IdVar}
END";

    public static readonly string Update = $@"
GO
CREATE PROCEDURE {UpdateUserAnimationProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {AnimationId} FROM {Schema}.{UserAnimationTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {AnimationId}={IdVar} WHERE {UserAuthCondition};
END";
}