using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;


namespace DatabaseStartup.Declaration.Item;

internal static class Animation
{
    public static readonly string Table = $@"
CREATE TABLE {AnimationTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(AnimationTable, ResourceTable)},
{Price}         INT                     NOT NULL    DEFAULT 100
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS A 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE A.{Id}={IdVar}
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllAnimationProc}
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS A
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateAnimationProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{AnimationTable}({ResourceId},{Name},{Detail},{Price}) 
    VALUES({IdVar},{NameVar},{DetailVar},{PriceVar});
END";

    public const string Function = $@"
--Animation
{Create}
{Select}
{SelectAll}";
}