using Common.Entity;

namespace WinFormsClient.Model;

public class VisualSoldItem : VisualDetailedItem
{
    public int Price { get; }
    public VisualSoldItem(SoldItem item, Image image) :
        base(item, image) =>
        Price = item.Price;
}

public sealed class VisualAnimation : VisualSoldItem
{
    public VisualAnimation(SoldItem item, Image image) : base(item, image)
    { }
}

public sealed class VisualCheckersSkin : VisualSoldItem
{
    public VisualCheckersSkin(SoldItem item, Image image) : base(item, image)
    { }
}

public sealed class VisualLootBox : VisualSoldItem
{
    public VisualLootBox(SoldItem item, Image image) : base(item, image)
    { }
}

public sealed class VisualAchievement : VisualDetailedItem
{
    public VisualAchievement(DetailedItem item, Image image) : base(item, image)
    {
    }
}