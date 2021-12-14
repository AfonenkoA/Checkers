using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.UserRepositoryBase;

namespace DatabaseStartup.Declaration.UserItem;

internal static class UserAchievement
{
    public static readonly string Table = $@"
CREATE TABLE {UserAchievementTable}
(
{Identity},
{AchievementId}   INT     NOT NULL    {Fk(UserAchievementTable, AchievementTable)},
{UserId}        INT     NOT NULL    {Fk(UserAchievementTable, UserTable)}
);";

    private const string SelectAll = @$"
GO
CREATE PROCEDURE {SelectAllUserAchievementProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{Id}, R.{ResourceExtension}
    FROM {Schema}.{UserAchievementTable} AS UA
    JOIN {AchievementTable} AS A ON UA.{AchievementId}=A.{Id}
    JOIN {ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE {UserId}={IdVar}
END";



    private const string Add = @$"";

    public const string Function = $@"
--UserAchievement
{Add}
{SelectAll}";
}