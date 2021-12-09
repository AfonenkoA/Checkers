using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;

namespace DatabaseStartup.Declaration;

internal static class Resource
{
    public static readonly string Table = $@"
CREATE TABLE {ResourceTable}
(
{Identity},
{ResourceExtension}     {StringType}	NOT NULL,
{ResourceBytes}         {BinaryType}	NOT NULL,
);";

    public static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectResourceProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{ResourceTable} WHERE {Id}={IdVar}
END";

    public static readonly string CreateFromFile = $@"
GO
CREATE PROCEDURE {CreateResourceFromFileProc} {PathVar} {StringType}
AS
BEGIN
    DECLARE {ResourceExtensionVar} {StringType}, {IdVar} INT, @sql {StringType}, {ResourceBytesVar} {BinaryType}
    SET {ResourceExtensionVar} = (SELECT RIGHT({PathVar}, CHARINDEX('.', REVERSE({PathVar}) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT {ResourceBytesVar} = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'{ResourceBytesVar} {BinaryType} OUT', {ResourceBytesVar} = {ResourceBytesVar} OUT;
    EXEC  {IdVar}={CreateResourceProc} {ResourceExtensionVar} ,{ResourceBytesVar}
    RETURN {IdVar}
END";

    public static readonly string Create = $@"
GO
CREATE PROCEDURE {CreateResourceProc} {ResourceExtensionVar} {StringType}, {ResourceBytesVar} {BinaryType}
AS
BEGIN
    INSERT INTO {Schema}.{ResourceTable}({ResourceExtension},{ResourceBytes}) 
    VALUES ({ResourceExtensionVar},{ResourceBytesVar});
    RETURN @@IDENTITY;
END";
}