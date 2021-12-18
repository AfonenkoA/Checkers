using System.Collections.Generic;
using System.Data.SqlClient;
using Common.Entity;
using static WebService.Repository.MSSqlImplementation.SqlExtensions;

namespace WebService.Repository.MSSqlImplementation;

public class UserRepositoryBase : RepositoryBase
{
    public const string SelectAllUserAchievementProc = "[SP_SelectUserAchievement]";
    public const string SelectAllUserCheckersSkinProc = "[SP_SelectAllUserCheckersSkin]";
    public const string SelectAllUserAnimationProc = "[SP_SelectAllUserAnimation]";
    public const string SelectUserPictureProc = "[SP_SelectUserPicture]";
    public const string SelectUserCheckersSkinProc = "[SP_SelectUserCheckersSkin]";
    public const string SelectUserAnimationProc = "[SP_SelectUserAnimation]";
    public const string SelectUserProc = "[SP_SelectUser]";

    protected UserRepositoryBase(SqlConnection connection) : base(connection)
    { }

    protected IEnumerable<Achievement> GetUserAchievements(int id)
    {
        using var command = CreateProcedure(SelectAllUserAchievementProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.GetAllAchievement();
    }

    protected IEnumerable<Animation> GetUserAnimations(int id)
    {
        using var command = CreateProcedure(SelectAllUserAnimationProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.GetAllAnimation();
    }

    protected IEnumerable<CheckersSkin> GetUserCheckerSkins(int id)
    {
        using var command = CreateProcedure(SelectAllUserCheckersSkinProc);
        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.GetAllCheckersSkin();
    }

    protected PublicUserData GetPublicUserDataUser(int userId)
    {
        var user = PublicUserData.Invalid;
        using (var command = CreateProcedure(SelectUserProc))
        {
            command.Parameters.Add(IdParameter(userId));

            using var reader = command.ExecuteReader();
            if (reader.Read())
                user = new PublicUserData(reader.GetUser());
            else
                return user;
        }
        user.SelectedAnimation = GetUserAnimation(userId);
        user.SelectedCheckers = GetUserCheckersSkin(userId);
        user.Picture = GetUserPicture(userId);
        user.Achievements = GetUserAchievements(userId);
        return user;
    }

    private Picture GetUserPicture(int userId)
    {
        using var command = CreateProcedure(SelectUserPictureProc);
        command.Parameters.Add(IdParameter(userId));
        using var reader = command.ExecuteReader();
        return reader.Read() ? reader.GetPicture() : Picture.Invalid;
    }

    private Animation GetUserAnimation(int userId)
    {
        using var command = CreateProcedure(SelectUserAnimationProc);
        command.Parameters.Add(IdParameter(userId));
        using var reader = command.ExecuteReader();
        return reader.Read() ? reader.GetAnimation() : Animation.Invalid;
    }

    private CheckersSkin GetUserCheckersSkin(int userId)
    {
        using var command = CreateProcedure(SelectUserCheckersSkinProc);
        command.Parameters.Add(IdParameter(userId));
        using var reader = command.ExecuteReader();
        return reader.Read() ? reader.GetCheckersSkin() : CheckersSkin.Invalid;
    }
}