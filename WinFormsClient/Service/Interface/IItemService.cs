using Common.Entity;
using WinFormsClient.Model.Item;

namespace WinFormsClient.Service.Interface;

internal interface IItemService
{
    public Task<VisualAnimation> Get(Animation animation);
    public Task<IEnumerable<VisualAnimation>> Get(IEnumerable<Animation> animations);
    public Task<VisualCheckersSkin> Get(CheckersSkin skin);
    public Task<IEnumerable<VisualCheckersSkin>> Get(IEnumerable<CheckersSkin> skins);
    public Task<VisualLootBox> Get(LootBox lootBox);
    public Task<IEnumerable<VisualLootBox>> Get(IEnumerable<LootBox> lootBoxes);
    public Task<VisualAchievement> Get(Achievement achievement);
    public Task<IEnumerable<VisualAchievement>> Get(IEnumerable<Achievement> achievements);
}