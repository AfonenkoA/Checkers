using static System.Console;
using static DatabaseStartup.Filling.CsvTable;

Write(
    @$"GO
CREATE DATABASE Checkers;

GO
{LoadAchievements()}
GO
{LoadAnimations()}
GO
{LoadLootBoxes()}
GO
{LoadPictures()}
GO
{LoadCheckersSkins()}
GO
{LoadUsers()}
GO
{LoadFriends()}
GO
{LoadFriendMessages()}

GO
{LoadNews()}
GO
{LoadNewsMessages()}
GO
{LoadPosts()}
GO
{LoadPostMessages()}
GO
{LoadCommonChat()}
");