using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;

namespace DatabaseStartup.Declaration.UserItem;


internal static class UserPicture
{
    private const string Select = $@"
GO
CREATE PROCEDURE {SelectUserPictureProc} {IdVar} INT
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}
    FROM {Schema}.{UserTable} AS U
    JOIN {Schema}.{PictureTable} AS P ON U.{PictureId}=P.{Id}
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
    WHERE U.{Id}={IdVar}
END";

    public const string Function = $@"
{Select}";
}