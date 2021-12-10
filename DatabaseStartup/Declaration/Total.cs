using DatabaseStartup.Declaration.UserItem;

namespace DatabaseStartup.Declaration;
using Item;

internal static class Total
{
    public static readonly string Table = $@"
--Table
{Resource.Table}
{Achievement.Table}
{Picture.Table}
{Animation.Table}
{Emotion.Table}
{CheckersSkin.Table}
{LootBox.Table}
{User.Type}
{User.Table}
{UserAchievement.Table}
{UserCheckersSkin.Table}
{UserAnimation.Table}
{Chat.Type}
{Chat.Table}
{Message.Table}
{Friendship.State}
{Friendship.Table}
{Post.Table}
{Article.Table}
";

    public static readonly string Function = $@"
--Functions
{Resource.Function}
{Friendship.Function}
{Chat.Function}
{User.Function}
{Message.Function}
{Achievement.Function}
{Animation.Function}
{Picture.Function}
{LootBox.Function}
{CheckersSkin.Function}
{UserAchievement.Function}
{UserAnimation.Function}
{UserCheckersSkin.Function}
{UserLootBox.Function}";
}