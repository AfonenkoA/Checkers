using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Declaration.UserItem;

internal class UserLootBox
{
    internal static readonly string Buy = $@"
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

    internal static readonly string GetAvailable = $@"
GO
CREATE PROCEDURE {UserGetAvailableLootBoxProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SELECT {Id} FROM {Schema}.{LootBoxTable} 
END";
}