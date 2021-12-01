using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class ItemRepository : Repository, IItemRepository
{
    public const string PictureTable = "[Picture]";
    public const string AchievementTable = "[Achievement]";
    public const string CheckersSkinTable = "[CheckersSkin]";
    public const string AnimationTable = "[Animation]";
    public const string LootBoxTable = "[LootBox]";

    public const string Name = "[name]";
    public const string Detail = "[detail]";
    public const string Price = "[price]";

    public const string AchievementId = "[achievement_id]";
    public const string CheckersSkinId = "[checkers_skin_id]";
    public const string AnimationId = "[animation_id]";
    public const string LootBoxId = "[lootbox_id]";
    public const string PictureId = "[picture_id]";

    public const string DetailVar = "@detail";
    public const string PriceVar = "@price";
    public const string PathVar = "@path";
    public const string NameVar = "@name";


    public const string CreatePictureProc = "[SP_CreatePicture]";
    public const string CreateAchievementProc = "[SP_CreateAchievement]";
    public const string CreateAnimationProc = "[SP_CreateAnimation]";
    public const string CreateCheckersSkinProc = "[SP_CreateCheckresSkin]";
    public const string CreateLootBoxProc = "[SP_CreateLootBox]";

    public const string SelectPictureProc = "[SP_SelectPicture]";
    public const string SelectAchievementProc = "[SP_SelectAchievement]";
    public const string SelectCheckersSkinProc = "[SP_SelectCheckersSkin]";
    public const string SelectLootBoxProc = "[SP_SelectLootBox]";
    public const string SelectAnimationProc = "[SP_SelectAnimation]";

    public const string SelectAllPictureProc = "[SP_SelectAllPicture]";
    public const string SelectAllAchievementProc = "[SP_SelectAllAchievement]";
    public const string SelectAllCheckersSkinProc = "[SP_SelectAllCheckersSkin]";
    public const string SelectAllLootBoxProc = "[SP_SelectAllLootBox]";
    public const string SelectAllAnimationProc = "[SP_SelectAllAnimation]";


    internal ItemRepository(SqlConnection connection) : base(connection) { }

    public IEnumerable<Achievement> GetAchievements()
    {
        using var command = CreateProcedure(SelectAllAchievementProc);
        using var reader = command.ExecuteReader();
        List<Achievement> list = new();
        while (reader.Read())
            list.Add(new Achievement(reader.GetDetailedItem()));
        return list;
    }

    public IEnumerable<Animation> GetAnimations()
    {
        using var command = CreateProcedure(SelectAllAnimationProc);
        using var reader = command.ExecuteReader();
        List<Animation> list = new();
        while (reader.Read())
            list.Add(new Animation(reader.GetSoldItem()));
        return list;
    }

    public IEnumerable<CheckersSkin> GetCheckerSkins()
    {
        using var command = CreateProcedure(SelectAllCheckersSkinProc);
        using var reader = command.ExecuteReader();
        List<CheckersSkin> list = new();
        while (reader.Read())
            list.Add(new CheckersSkin(reader.GetSoldItem()));
        return list;
    }

    public IEnumerable<LootBox> GetLootBoxes()
    {
        using var command = CreateProcedure(SelectAllLootBoxProc);
        using var reader = command.ExecuteReader();
        List<LootBox> list = new();
        while (reader.Read())
            list.Add(new LootBox(reader.GetSoldItem()));
        return list;
    }

    public IEnumerable<Picture> GetPictures()
    {
        using var command = CreateProcedure(SelectAllPictureProc);
        using var reader = command.ExecuteReader();
        List<Picture> list = new();
        while (reader.Read())
            list.Add(new Picture(reader.GetNamedItem()));
        return list;
    }

    public Achievement GetAchievement(int id)
    {
        using var command = CreateProcedure(SelectAchievementProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? new Achievement(reader.GetDetailedItem()) : Achievement.Invalid;
    }

    public Animation GetAnimation(int id)
    {
        using var command = CreateProcedure(SelectAnimationProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? new Animation(reader.GetSoldItem()) : Animation.Invalid;
    }

    public CheckersSkin GetCheckersSkin(int id)
    {
        using var command = CreateProcedure(SelectCheckersSkinProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? new CheckersSkin(reader.GetSoldItem()) : CheckersSkin.Invalid;
    }

    public LootBox GetLootBox(int id)
    {
        using var command = CreateProcedure(SelectLootBoxProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? new LootBox(reader.GetSoldItem()) : LootBox.Invalid;
    }

    public Picture GetPicture(int id)
    {
        using var command = CreateProcedure(SelectPictureProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? new Picture(reader.GetNamedItem()) : Picture.Invalid;
    }
}