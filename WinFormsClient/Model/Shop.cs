using WinFormsClient.Model.Item;
using WinFormsClient.Model.Item.Shop;

namespace WinFormsClient.Model;

public class Shop
{
    public IEnumerable<ShopAnimation> Animations;
    public IEnumerable<ShopCheckersSkin> Skins;
    public IEnumerable<VisualLootBox> LootBoxes;

    public Shop(IEnumerable<ShopAnimation> animations,
        IEnumerable<ShopCheckersSkin> skins,
        IEnumerable<VisualLootBox> lootBoxes)
    {
        this.Animations = animations;
        Skins = skins;
        LootBoxes = lootBoxes;
    }
}