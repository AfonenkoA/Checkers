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

    public static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT CH.*, R.{ResourceExtension}
    FROM {Schema}.{CheckersSkinTable} AS CH 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=CH.{ResourceId}
    WHERE CH.{Id}={IdVar}
END";

    public static readonly string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllCheckersSkinProc}
AS
BEGIN
    SELECT CH.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS CH
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=CH.{ResourceId}
END";
}