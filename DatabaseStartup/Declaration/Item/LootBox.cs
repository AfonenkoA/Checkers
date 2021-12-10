using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;


namespace DatabaseStartup.Declaration.Item;

internal static class LootBox
{
    public static readonly string Table = @$"
CREATE TABLE {LootBoxTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(LootBoxTable, ResourceTable)},
{Price}         INT                     NOT NULL    DEFAULT 100
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectLootBoxProc} {IdVar} INT
AS
BEGIN
    SELECT L.*, R.{ResourceExtension}
    FROM {Schema}.{LootBoxTable} AS L
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=L.{ResourceId}
    WHERE L.{Id}={IdVar}
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllLootBoxProc}
AS
BEGIN
    SELECT L.*, R.{ResourceExtension}
    FROM {Schema}.{LootBoxTable} AS L
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=L.{ResourceId}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateLootBoxProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{LootBoxTable}({ResourceId},{Name},{Detail},{Price}) 
    VALUES({IdVar},{NameVar},{DetailVar},{PriceVar});
END";

    public static readonly string Function = $@"
--LootBox
{Create}
{Select}
{SelectAll}";
}