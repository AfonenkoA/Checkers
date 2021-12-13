namespace ApiContract;

public static class Route
{
    public const string UserRoute = "newuser";
    public const string ItemRoute = "item";
    public const string StatisticsRoute = "stat";
    public const string ChatRoute = "chat";
    public const string NewsRoute = "news";
    public const string ForumRoute = "forum";
    public const string ResourceRoute = "res";
    public const string GameRoute = "game";

    public const string AchievementRoute = $"{ItemRoute}/achievement";
    public const string AnimationRoute = $"{ItemRoute}/animation";
    public const string CheckersSkinRoute = $"{ItemRoute}/checkers-skin";
    public const string LootBoxRoute = $"{ItemRoute}/lootbox";
    public const string PictureRoute = $"{ItemRoute}/picture";

    public const string ChatPublic = $"{ChatRoute}/public";

}