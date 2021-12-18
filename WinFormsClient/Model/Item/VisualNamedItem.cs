using Common.Entity;

namespace WinFormsClient.Model.Item;

public class VisualNamedItem
{
    internal int Id { get; }
    internal string Name { get; }
    internal Image Image { get; }

    public VisualNamedItem(NamedItem item, Image img)
    {
        Id = item.Id;
        Name = item.Name;
        Image = img;
    }

    public VisualNamedItem(VisualNamedItem item)
    {
        Id = item.Id;
        Name = item.Name;
        Image = item.Image;
    }
}