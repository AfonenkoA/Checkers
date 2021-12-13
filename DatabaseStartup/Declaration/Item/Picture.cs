using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;


namespace DatabaseStartup.Declaration.Item;

internal static class Picture
{
    public static readonly string Table = $@"
CREATE TABLE {PictureTable}
(
{Identity},
{ItemName}          {UniqueStringType}      NOT NULL    UNIQUE,
{ResourceId}    INT                     NOT NULL    {Fk(PictureTable, ResourceTable)}
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectPictureProc} {IdVar} INT
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}, R.{Id}
    FROM {Schema}.{PictureTable} AS P
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
    WHERE P.{Id}={IdVar}
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllPictureProc}
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}
    FROM {Schema}.{PictureTable} AS P
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreatePictureProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{PictureTable}({ResourceId},{ItemName}) 
    VALUES ({IdVar},{NameVar});
END";

    public const string Function = $@"
--Picture
{Create}
{Select}
{SelectAll}";
}