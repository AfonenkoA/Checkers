using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration.Item;

internal static class Achievement
{
    public static readonly string Table = $@"
CREATE TABLE {AchievementTable}
(
{Identity},
{ItemName}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(AchievementTable, ResourceTable)}
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectAchievementProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AchievementTable} AS A 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE A.{Id}={IdVar}
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllAchievementProc}
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AchievementTable} AS A
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateAchievementProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{AchievementTable}({ResourceId},{ItemName},{Detail}) 
    VALUES({IdVar},{NameVar},{DetailVar});
END";

    public const string Function = $@"
--Achievement
{Create}
{Select}
{SelectAll}";
}