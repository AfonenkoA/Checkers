using DatabaseStartup.Filling.Entity;
using static DatabaseStartup.Filling.Common;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Filling;

internal class Item
{
    private const string PictureSource = "AvatarPicture.csv";
    private const string CheckersSource = "CheckersSkins.csv";
    private const string AnimationSource = "Animations.csv";
    private const string AchievementsSource = "Achivements.csv";
    private const string LootBoxSource = "LootBoxes.csv";

    private static string LoadPictures() =>
        string.Join('\n', ReadLines(DataFile(PictureSource))
            .Select(s => Exec(CreatePictureProc, new NamedItemArgs(s))));

    private static string LoadAchievements() =>
        string.Join('\n', ReadLines(DataFile(AchievementsSource))
            .Select(s => Exec(CreateAchievementProc, new DetailedItemArgs(s))));

    private static string LoadAnimations() =>
        string.Join('\n', ReadLines(DataFile(AnimationSource))
            .Select(s => Exec(CreateAnimationProc, new SoldItemArgs(s))));

    private static string LoadLootBoxes() =>
        string.Join('\n', ReadLines(DataFile(LootBoxSource))
            .Select(s => Exec(CreateLootBoxProc, new SoldItemArgs(s))));

    private static string LoadCheckersSkins() =>
        string.Join('\n', ReadLines(DataFile(CheckersSource))
            .Select(s => Exec(CreateCheckersSkinProc, new SoldItemArgs(s))));

    public static readonly string Total = $@"
{LoadAchievements()}
{LoadAnimations()}
{LoadCheckersSkins()}
{LoadPictures()}
{LoadLootBoxes()}";
}