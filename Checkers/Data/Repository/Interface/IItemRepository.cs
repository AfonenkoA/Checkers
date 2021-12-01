using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

public interface IItemRepository
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