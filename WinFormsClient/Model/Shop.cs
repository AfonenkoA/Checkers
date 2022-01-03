using WinFormsClient.Model.Item;
using WinFormsClient.Model.Item.Shop;

namespace WinFormsClient.Model;

public class Shop
{
    public readonly IEnumerable<ShopAnimation> Animations;
    public readonly IEnumerable<ShopCheckersSkin> Skins;
    public readonly IEnumerable<VisualLootBox> LootBoxes;
    public readonly int Currency;

    public Shop(IEnumerable<ShopAnimation> animations,
        IEnumerable<ShopCheckersSkin> skins,
        IEnumerable<VisualLootBox> lootBoxes, int currency)
    {
        Animations = animations;
        Skins = skins;
        LootBoxes = lootBoxes;
        Currency = currency;
    }
}