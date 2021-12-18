using Common.Entity;

namespace WinFormsClient.Model.Item;

public class VisualLootBox : VisualSoldItem
{
    public VisualLootBox(SoldItem item, Image image) : base(item, image)
    { }

    public VisualLootBox(VisualLootBox box) : base(box)
    { }
}