using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration.UserItem;

internal static class UserCheckersSkin
{
    public static readonly string Table = $@"
CREATE TABLE {UserCheckersSkinTable}
(
{Identity},
{CheckersSkinId}   INT     NOT NULL    {Fk(UserCheckersSkinTable, CheckersSkinTable)},
{UserId}        INT     NOT NULL    {Fk(UserCheckersSkinTable, UserTable)}
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectUserCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT C.*, R.{Id}, R.{ResourceExtension} FROM {Schema}.{UserCheckersSkinTable} AS UC
    JOIN {CheckersSkinTable} AS C ON UC.{CheckersSkinId}=C.{Id}
    JOIN {ResourceTable} AS R ON R.{Id}=C.{ResourceId}
    WHERE {UserId}={IdVar}
END";

    private const string Update = $@"
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

    internal const string Add = $@"
GO
CREATE PROCEDURE {UserAddCheckersSkinProc} {UserIdVar} INT, {IdVar} INT
AS
BEGIN
    INSERT INTO {UserCheckersSkinTable}({UserId},{CheckersSkinId}) VALUES({UserIdVar},{IdVar}) 
END";

    private const string Buy = $@"
GO
CREATE PROCEDURE {UserBuyCheckersSkinProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{CheckersSkinTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        BEGIN
        EXEC {UserAddCheckersSkinProc} {UserIdVar}, {IdVar}
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
        END
END";

    private const string GetAvailable = $@"
GO
CREATE PROCEDURE {UserGetAvailableCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT C.*, R.{ResourceExtension}, R.{Id} FROM {Schema}.{CheckersSkinTable} AS C
    JOIN {Schema}.{ResourceTable} AS R ON C.{ResourceId}=R.{Id}
    EXCEPT
    SELECT C.*, R.{ResourceExtension}, R.{Id} FROM {Schema}.{UserCheckersSkinTable} AS UC
    JOIN {Schema}.{CheckersSkinTable} AS C ON C.{Id} = UC.{CheckersSkinId}
    JOIN {Schema}.{ResourceTable} AS R ON C.{ResourceId}=R.{Id}
    WHERE UC.{UserId}={IdVar}
END";

    public const string Function = $@"
--UserCheckersSkin
{Select}
{Update}
{GetAvailable}
{Buy}";
}