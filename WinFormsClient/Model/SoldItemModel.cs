using Common.Entity;

namespace WinFormsClient.Model;

public class VisualSoldItem : VisualDetailedItem
{
    public int Price { get; }
    public VisualSoldItem(SoldItem item, Image image) :
        base(item, image) => Price = item.Price;

    public VisualSoldItem(VisualSoldItem item) : base(item)
    {
        Price = item.Price;
    }
}

public class VisualAnimation : VisualSoldItem
{
    public VisualAnimation(SoldItem item, Image image) : base(item, image)
    { }

    public VisualAnimation(VisualAnimation a) : base(a) { }
}

public class VisualCheckersSkin : VisualSoldItem
{
    public VisualCheckersSkin(SoldItem item, Image image) : base(item, image)
    { }
    public VisualCheckersSkin(VisualCheckersSkin item) : base(item) { }
}

public interface ISelectable
{
    public bool IsSelected { get; }
}

public sealed class CollectionCheckersSkin : VisualCheckersSkin, ISelectable
{

    public CollectionCheckersSkin(VisualCheckersSkin skin, bool isSelected) : base(skin)
    {
        IsSelected = isSelected;
    }
    public bool IsSelected { get; }
}

public sealed class CollectionAnimation : VisualAnimation, ISelectable
{

    public CollectionAnimation(VisualAnimation animation, bool isSelected) : base(animation)
    {
        IsSelected = isSelected;
    }

    public bool IsSelected { get; }
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

