using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncItemApi
{
    Task<(bool,IEnumerable<Achievement>)> TryGetAchievements();
    Task<(bool, IEnumerable<Animation>)> TryGetAnimations();
    Task<(bool, IEnumerable<CheckersSkin>)> TryGetCheckerSkins();
    Task<(bool, IEnumerable<LootBox>)> TryGetLootBoxes();
    Task<(bool, IEnumerable<Picture>)> TryGetPictures();

    Task<(bool, Achievement)> TryGetAchievement(int id);
    Task<(bool, Animation)> TryGetAnimation(int id);
    Task<(bool, CheckersSkin)> TryGetCheckersSkin(int id);
    Task<(bool, LootBox)> TryGetLootBox(int id);
    Task<(bool, Picture)> TryGetPicture(int id);
}