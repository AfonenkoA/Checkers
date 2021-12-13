using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

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

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectUserAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{ResourceExtension} FROM {Schema}.{UserAnimationTable} AS UA
    JOIN {AnimationTable} AS A ON UA.{AnimationId}=A.{Id}
    JOIN {ResourceTable} AS R ON A.{ResourceId}=R.{Id}
    WHERE {UserId}={IdVar}
END";

    private const string Update = $@"
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

    internal const string Add = $@"
GO
CREATE PROCEDURE {UserAddAnimationProc} {UserIdVar} INT, {IdVar} INT
AS
BEGIN
    INSERT INTO {UserAnimationTable}({UserId},{AnimationId}) VALUES({UserIdVar},{IdVar}) 
END";

    private const string Buy = $@"
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

    private const string GetAvailable = $@"
GO
CREATE PROCEDURE {UserGetAvailableAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{Id}, R.{ResourceExtension} FROM {Schema}.{AnimationTable} AS A
    JOIN {ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    EXCEPT
    SELECT A.*, R.{Id}, R.{ResourceExtension} FROM {Schema}.{UserAnimationTable} AS UA
    JOIN {Schema}.{AnimationTable} AS A ON A.{Id}=UA.{AnimationId}
    JOIN {ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE UA.{UserId}={IdVar}
END";

    public const string Function = $@"
--UserAnimation
{Select}
{Update}
{GetAvailable}
{Buy}";
}