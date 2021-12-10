using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.Repository;

namespace DatabaseStartup.Declaration.UserItem;

internal static class UserAchievement
{
    public static readonly string Table = $@"
CREATE TABLE {UserAchievementTable}
(
{Identity},
{AchievementId}   INT     NOT NULL    {Fk(UserAchievementTable, AnimationTable)},
{UserId}        INT     NOT NULL    {Fk(UserAchievementTable, UserTable)}
);";

    private const string Select = @$"
GO
CREATE PROCEDURE {SelectUserAchievementProc} {IdVar} INT
AS
BEGIN
    SELECT A.* FROM {Schema}.{UserAchievementTable} AS UA
    JOIN {AchievementTable} AS A ON UA.{AchievementId}=A.{Id}
    WHERE {Id}={IdVar}
END";

    private const string Add = @$"";

    public const string Function = $@"
--UserAchievement
{Add}
{Select}";
}