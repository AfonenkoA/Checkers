using Common.Entity;

namespace WinFormsClient.Model;

public class VisualDetailedItem : VisualNamedItem
{
    public string Detail { get; }

    public VisualDetailedItem(DetailedItem item, Image image) : 
        base(item, image) =>
        Detail = item.Detail;

    public VisualDetailedItem(VisualDetailedItem item) : base(item)
    {
        Detail = item.Detail;
    }
}