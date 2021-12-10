using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;


namespace DatabaseStartup.Declaration.Item;

internal static class CheckersSkin
{
    public static readonly string Table = $@"
CREATE TABLE {CheckersSkinTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(CheckersSkinTable, ResourceTable)},
{Price}         INT                     NOT NULL    DEFAULT 100
);";

    private static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT CH.*, R.{ResourceExtension}
    FROM {Schema}.{CheckersSkinTable} AS CH 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=CH.{ResourceId}
    WHERE CH.{Id}={IdVar}
END";

    private static readonly string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllCheckersSkinProc}
AS
BEGIN
    SELECT CH.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS CH
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=CH.{ResourceId}
END";

    private static readonly string Create = $@"
GO
CREATE PROCEDURE {CreateCheckersSkinProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{CheckersSkinTable}({ResourceId},{Name},{Detail},{Price}) 
    VALUES({IdVar},{NameVar},{DetailVar},{PriceVar});
END";

    public static readonly string Function = $@"
--CheckersSkin
{Create}
{Select}
{SelectAll}";
}