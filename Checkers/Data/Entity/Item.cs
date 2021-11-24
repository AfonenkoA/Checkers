using System;
using System.Text.Json.Serialization;

namespace Checkers.Data.Entity;

public enum ItemType
{
    Picture,
    Achievement,
    CheckersSkin,
    Animation,
    LootBox,
    
}

public class ItemHash
{
    public int Id { get; set; } = -1;
    public DateTime Updated { get; set; } = DateTime.MinValue;
    
    [JsonIgnore]
    public virtual bool IsValid => !(Id == -1 || Updated == DateTime.MinValue);
}

public class ItemInfo : ItemHash
{
    public ItemType Type { get; set; }
    public int Price { get; set; } = -1;
    public static readonly ItemInfo Invalid = new();
    public string Extension { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;

    [JsonIgnore]
    public override bool IsValid => base.IsValid &&
                               !(Name == string.Empty ||
                                 Detail == string.Empty ||
                                 Extension == string.Empty ||
                                 Price == -1);
}

public sealed class Item : ItemInfo
{
    public byte[] Image { get; set; } = Array.Empty<byte>();
}