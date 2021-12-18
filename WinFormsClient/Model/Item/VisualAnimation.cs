using Common.Entity;

namespace WinFormsClient.Model.Item;

public class VisualAnimation : VisualSoldItem
{
    public VisualAnimation(SoldItem item, Image image) : base(item, image)
    { }

    public VisualAnimation(VisualAnimation a) : base(a) { }
}