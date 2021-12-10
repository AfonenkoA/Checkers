using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Declaration.UserItem;

internal static class UserAnimation
{
    public static readonly string Table = $@"
CREATE TABLE {UserAnimationTable}
(
{Identity},
{AnimationId}   INT     NOT NULL    {Fk(UserAnimationTable, AnimationTable)},
{UserId}        INT     NOT NULL    {Fk(UserAnimationTable, UserTable)}
);";

    private static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectUserAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT A.* FROM {Schema}.{UserAnimationTable} AS UA
    JOIN {AnimationTable} AS A ON UA.{AnimationId}=A.{Id} 
    WHERE {Id}={IdVar}
END";

    private static readonly string Update = $@"
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

    private static readonly string Add = $@"
GO
USE Checkers;
GO
CREATE PROCEDURE {UserAddAnimationProc} {UserIdVar} INT, {IdVar} INT
AS
BEGIN
    INSERT INTO {UserAnimationTable}({UserId},{AnimationId}) VALUES({UserIdVar},{IdVar}) 
END";

    private static readonly string Buy = $@"
GO
CREATE PROCEDURE {UserBuyAnimationProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{AnimationTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        BEGIN
        EXEC {UserAddAnimationProc} {UserIdVar}, {IdVar}
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
        END
END";

    private static readonly string GetAvailable = $@"
GO
CREATE PROCEDURE {UserGetAvailableAnimationProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SELECT {Id} FROM {Schema}.{AnimationTable} 
    EXCEPT
    SELECT {AnimationId} FROM {Schema}.{UserAnimationTable}
END";

    public static readonly string Function = $@"
--UserAnimation
{Add}
{Select}
{Update}
{GetAvailable}
{Buy}";
}