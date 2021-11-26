using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.ResourceRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class ItemRepository : Repository, IItemRepository
{
    public const string ItemTable = "[Item]";
    public const string ItemTypeTable = "[ItemType]";

    public const string ItemTypeVar = "@item_type";

    public static readonly string PictureTable = $"[{ItemType.Picture}]";
    public static readonly string AchievementTable = $"[{ItemType.Achievement}]";
    public static readonly string CheckersTable = $"[{ItemType.CheckersSkin}]";
    public static readonly string AnimationTable = $"[{ItemType.Animation}]";
    public static readonly string LootBoxTable = $"[{ItemType.LootBox}]";
    public const string ItemId = "[item_id]";

    public const string Detail = "[detail]";
    public const string ItemName = "[item_name]";
    public const string ItemTypeName = "[item_type_name]";
    public const string ItemTypeId = "[item_type_id]";
    public const string Updated = "[updated]";
    public const string Price = "[price]";

    public const string DetailVar = "@detail";
    public const string ItemNameVar = "@item_name";
    public const string PriceVar = "@price";
    public const string PathVar = "@path";
    public const string ItemTypeNameVar = "@item_type_name";
    public const string ItemTypeIdVar = "@item_type_id";

    public const string SelectItemsProc = "[SP_SelectItems]";
    public const string SelectItemProc = "[SP_SelectItem]";
    public const string SelectItemPictureProc = "[SP_SelectItemPicture]";
    public const string CreateItemProc = "[SP_CreateItem]";
    public const string SetItemsProc = "[SP_SetItems]";
    public const string GetItemTypeByNameProc = "[SP_GetItemTypeByName]"; 

    public const string ItemTypeCursor = "ItemTypeCursor";

    public static readonly ItemRepository Instance = new();
    public IEnumerable<ItemHash> GetItems()
    {
        using var command = CreateProcedure(SelectItemsProc);
        using var reader = command.ExecuteReader();
        List<ItemHash> items = new();
        while (reader.Read())
            items.Add(new ItemHash {Id = reader.GetFieldValue<int>(Id),
                Updated = reader.GetFieldValue<DateTime>(Updated)});
        return items;
    }

    public ItemInfo GetItemInfo(int id)
    {
        using var command = CreateProcedure(SelectItemProc);
        command.Parameters.Add(new SqlParameter {ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = id});
        using var reader = command.ExecuteReader();
        if (reader.Read())
            return new ItemInfo
            {
                Id = reader.GetFieldValue<int>(Id),
                Detail = reader.GetFieldValue<string>(Detail),
                Extension = reader.GetFieldValue<string>(ResourceExtension),
                Name = reader.GetFieldValue<string>(ItemName),
                Type = (ItemType) reader.GetFieldValue<int>(ItemTypeId),
                Updated = reader.GetFieldValue<DateTime>(Updated),
                Price = reader.GetFieldValue<int>(Price)
            };
        return ItemInfo.Invalid;
    }

    public (byte[],string) GetItemImage(int id)
    {
        using var command = CreateProcedure(SelectItemPictureProc);
        command.Parameters.Add(new SqlParameter { ParameterName = IdVar, SqlDbType = SqlDbType.Int, Value = id });
        using var reader = command.ExecuteReader();
        return reader.Read() ? 
            (reader.GetFieldValue<byte[]>(ResourceBytes), reader.GetFieldValue<string>(ResourceExtension)) : 
            (Array.Empty<byte>(), string.Empty);
    }
}