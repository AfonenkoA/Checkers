using System;
using System.Linq;
using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public enum ItemType
{
    Picture=1,
    Achievement,
    CheckersSkin,
    Animation,
    LootBox,
    Invalid
}

public class ItemHash
{
    public int Id { get; init; } = InvalidId;
    public DateTime Updated { get; init; } = InvalidDate;

    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidId ||
                                     Updated == InvalidDate);
}

public class ItemInfo : ItemHash
{
    public static readonly ItemInfo Invalid = new();

    public ItemType Type { get; init; }
    public int Price { get; init; } = InvalidId;
    public string Extension { get; init; } = InvalidString;
    public string Name { get; init; } = InvalidString;
    public string Detail { get; init; } = InvalidString;

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
    public byte[] Image { get; init; } = Array.Empty<byte>();

    [JsonIgnore] public override bool IsValid => base.IsValid && Image.Any();

}