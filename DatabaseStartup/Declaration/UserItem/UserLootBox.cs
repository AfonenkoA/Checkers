using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration.UserItem;

internal static class UserLootBox
{
    private const string Buy = $@"
GO
CREATE PROCEDURE {UserBuyLootBoxProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{LootBoxTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
END";

    private const string GetAvailable = $@"
GO
CREATE PROCEDURE {UserGetAvailableLootBoxProc} {IdVar} INT
AS
BEGIN
    SELECT L.*, R.{ResourceExtension}, R.{Id} FROM {Schema}.{LootBoxTable} AS L
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=L.{ResourceId}
END";

    public const string Function = $@"
--UserLootBox
{GetAvailable}
{Buy}";
}