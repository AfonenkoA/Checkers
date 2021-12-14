using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration.Item;

internal static class Emotion
{
    public static readonly string Table = $@"
CREATE TABLE {EmotionTable}
(
{Identity},
{ItemName}      {UniqueStringType}      NOT NULL    UNIQUE,
{ResourceId}    INT                     NOT NULL    {Fk(EmotionTable, ResourceTable)}
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectEmotionProc} {IdVar} INT
AS
BEGIN
    SELECT E.*, R.{ResourceExtension}
    FROM {Schema}.{EmotionTable} AS E
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=E.{ResourceId}
    WHERE E.{Id}={IdVar}
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllEmotionProc}
AS
BEGIN
    SELECT E.*, R.{ResourceExtension}
    FROM {Schema}.{EmotionTable} AS E
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=E.{ResourceId}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateEmotionProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{EmotionTable}({ResourceId},{ItemName}) 
    VALUES ({IdVar},{NameVar});
END";

    public const string Function = $@"
--Emotion
{Create}
{Select}
{SelectAll}";
}