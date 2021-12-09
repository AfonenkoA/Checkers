using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration.Item;

internal class Achievement
{
    public static readonly string Table = $@"
CREATE TABLE {AchievementTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(AchievementTable, ResourceTable)}
);";

    public static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectAchievementProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AchievementTable} AS A 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE A.{Id}={IdVar}
END";

    public static readonly string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllAchievementProc}
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AchievementTable} AS A
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
END";
}