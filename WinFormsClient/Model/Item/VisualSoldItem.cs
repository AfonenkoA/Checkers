using Common.Entity;

namespace WinFormsClient.Model.Item;

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