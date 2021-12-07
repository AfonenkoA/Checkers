using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public sealed class ResourceInfo
{
    public static readonly ResourceInfo Invalid = new();

    public int Id { get; init; } = InvalidId;
    public string Extension { get; init; } = InvalidString;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidId ||
                             Extension == InvalidString);
}

public class Item
{
    public static readonly Item Invalid = new();
    public int Id { get; init; } = InvalidId;
    public ResourceInfo Resource { get; init; } = ResourceInfo.Invalid;

    [JsonIgnore] public virtual bool IsValid => Id != InvalidId && Resource.IsValid;

}

public class NamedItem : Item
{
    public new static NamedItem Invalid = new();
    public string Name { get; init; } = InvalidString;

    public NamedItem(Item item)
    {
        Id = item.Id;
        Resource = item.Resource;
    }

    [JsonConstructor]
    public NamedItem(){}

    [JsonIgnore] public override bool IsValid => base.IsValid && Name != InvalidString;
}

public class DetailedItem : NamedItem
{
    public new static DetailedItem Invalid = new();
    public string Detail { get; init; } = InvalidString;

    public DetailedItem(NamedItem item) : base(item)
    {
        Name = item.Name;
    }

    [JsonConstructor]
    public DetailedItem() { }

    [JsonIgnore] public override bool IsValid => base.IsValid && Name != InvalidString;
}

public class SoldItem : DetailedItem
{
    public new static readonly SoldItem Invalid = new();
    public int Price { get; init; } = InvalidId;

    public SoldItem(DetailedItem item) : base(item)
    {
        Name = item.Name;
        Detail = item.Detail;
    }

    [JsonConstructor]
    public SoldItem(){}

    [JsonIgnore] 
    public override bool IsValid => base.IsValid && Price!=InvalidId;
}

public sealed class Achievement : DetailedItem
{
    public new static Achievement Invalid = new();
    public Achievement(DetailedItem item) : base(item)
    {
        Name = item.Name;
        Detail = item.Detail;
    }

    [JsonConstructor]
    public Achievement() { }
}

public sealed class Picture : NamedItem
{
    public new static Picture Invalid = new();
    public Picture(NamedItem item) : base(item)
    {
        Name = item.Name;
    }
    [JsonConstructor]
    public Picture() { }
}

public sealed class Animation : SoldItem
{
    public new static Animation Invalid = new();
    public Animation(SoldItem item) : base(item)
    {
        Price = item.Price;
    }
    [JsonConstructor]
    public Animation() { }
}

public sealed class CheckersSkin : SoldItem
{
    public new static CheckersSkin Invalid = new();
    public CheckersSkin(SoldItem item) : base(item)
    {
        Price = item.Price;
    }
    [JsonConstructor]
    public CheckersSkin() { }
}

public sealed class LootBox : SoldItem
{
    public new static LootBox Invalid = new();
    public LootBox(SoldItem item) : base(item)
    {
        Price = item.Price;
    }
    [JsonConstructor]
    public LootBox() { }
}

public sealed class Emotion : NamedItem
{
    public new static Emotion Invalid = new();
    public Emotion(NamedItem item) : base(item)
    {
        Name = item.Name;
    }
    [JsonConstructor]
    public Emotion() { }
}