using System.Text.Json.Serialization;
using static Common.Entity.EntityValues;

namespace Common.Entity;

public sealed class ResourceInfo
{
    public static readonly ResourceInfo Invalid = new();

    public int Id { get; init; } = InvalidInt;
    public string Extension { get; init; } = InvalidString;

    [JsonIgnore]
    public bool IsValid => !(Id == InvalidInt ||
                             Extension == InvalidString);
}

public class Item
{
    public static readonly Item Invalid = new();
    public int Id { get; init; } = InvalidInt;
    public ResourceInfo Resource { get; init; } = ResourceInfo.Invalid;

    [JsonIgnore] public virtual bool IsValid => Id != InvalidInt && Resource.IsValid;

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
    public int Price { get; init; } = InvalidInt;

    public SoldItem(DetailedItem item) : base(item)
    {
        Name = item.Name;
        Detail = item.Detail;
    }

    [JsonConstructor]
    public SoldItem(){}

    [JsonIgnore] 
    public override bool IsValid => base.IsValid && Price!=InvalidInt;
}

public sealed class Achievement : DetailedItem
{
    public new static readonly Achievement Invalid = new();
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
    public new static readonly Picture Invalid = new();
    public Picture(NamedItem item) : base(item)
    {
        Name = item.Name;
    }
    [JsonConstructor]
    public Picture() { }
}

public sealed class Animation : SoldItem
{
    public new static readonly Animation Invalid = new();
    public Animation(SoldItem item) : base(item)
    {
        Price = item.Price;
    }
    [JsonConstructor]
    public Animation() { }
}

public sealed class CheckersSkin : SoldItem
{
    public new static readonly CheckersSkin Invalid = new();
    public CheckersSkin(SoldItem item) : base(item)
    {
        Price = item.Price;
    }
    [JsonConstructor]
    public CheckersSkin() { }
}

public sealed class LootBox : SoldItem
{
    public new static readonly LootBox Invalid = new();
    public LootBox(SoldItem item) : base(item)
    {
        Price = item.Price;
    }
    [JsonConstructor]
    public LootBox() { }
}

public sealed class Emotion : NamedItem
{
    public new static readonly Emotion Invalid = new();
    public Emotion(NamedItem item) : base(item)
    {
        Name = item.Name;
    }
    [JsonConstructor]
    public Emotion() { }
}