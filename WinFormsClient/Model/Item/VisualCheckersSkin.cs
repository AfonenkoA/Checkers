using Common.Entity;

namespace WinFormsClient.Model.Item;

public class VisualCheckersSkin : VisualSoldItem
{
    public VisualCheckersSkin(SoldItem item, Image image) : base(item, image)
    { }
    public VisualCheckersSkin(VisualCheckersSkin item) : base(item) { }
}