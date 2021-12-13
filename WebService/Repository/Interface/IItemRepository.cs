using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IItemRepository
{
    IEnumerable<Achievement> GetAchievements();
    IEnumerable<Animation> GetAnimations();
    IEnumerable<CheckersSkin> GetCheckerSkins();
    IEnumerable<LootBox> GetLootBoxes();
    IEnumerable<Picture> GetPictures();

    Achievement GetAchievement(int id);
    Animation GetAnimation(int id);
    CheckersSkin GetCheckersSkin(int id);
    LootBox GetLootBox(int id);
    Picture GetPicture(int id);
}