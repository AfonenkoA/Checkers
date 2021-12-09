using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;


namespace DatabaseStartup.Declaration.Item;

internal class Picture
{
    public static readonly string Table = $@"
CREATE TABLE {PictureTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{ResourceId}    INT                     NOT NULL    {Fk(PictureTable, ResourceTable)}
);";

    public static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectPictureProc} {IdVar} INT
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}
    FROM {Schema}.{PictureTable} AS P
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
    WHERE P.{Id}={IdVar}
END";

    public static readonly string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllPictureProc}
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}
    FROM {Schema}.{PictureTable} AS P
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
END";
}