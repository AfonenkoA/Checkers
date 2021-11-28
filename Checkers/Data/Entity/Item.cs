using System;
using System.Linq;
using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public enum ItemType
{
    Picture,
    Achievement,
    CheckersSkin,
    Animation,
    LootBox,
    Invalid
}

public class ItemHash
{
    public int Id { get; set; } = InvalidId;
    public DateTime Updated { get; set; } = InvalidDate;
    
    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidId ||
                                     Updated == InvalidDate);
}

public class ItemInfo : ItemHash
{
    public static readonly ItemInfo Invalid = new();

    public ItemType Type { get; set; }
    public int Price { get; set; } = InvalidId;
    public string Extension { get; set; } = InvalidString;
    public string Name { get; set; } = InvalidString;
    public string Detail { get; set; } = InvalidString;

    [JsonIgnore]
    public override bool IsValid => base.IsValid &&
                               !(Name == InvalidString ||
                                 Detail == InvalidString ||
                                 Extension == InvalidString ||
                                 Price == InvalidId || 
                                 Type == ItemType.Invalid);
}

public sealed class Item : ItemInfo
{
    public byte[] Image { get; set; } = Array.Empty<byte>();

    [JsonIgnore] public override bool IsValid => base.IsValid && Image.Any();

}