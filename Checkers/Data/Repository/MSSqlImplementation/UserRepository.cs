using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class UserRepository : Repository, IUserRepository
{
    public const string UserTable = "[User]";
    public const string UserItemExtendedView = "[UserItemExtended]";
    public const string UserTypeTable = "[UserType]";
    public const string FriendshipStateTable = "[FriendshipState]";
    public const string FriendshipTable = "[Friendship]";

    public const string UserAchievementTable = "[UserAchievement]";
    public const string UserCheckersSkinTable = "[UserCheckerSkin]";
    public const string UserAnimationTable = "[UserAnimation]";


    public const string Nick = "[nick]";
    public const string Login = "[login]";
    public const string Password = "[password]";
    public const string SocialCredit = "[social_credit]";
    public const string LastActivity = "[last_activity]";

    public const string UserId = "[user_id]";
    public const string UserTypeId = "[user_type_id]";
    public const string UserTypeName = "[user_type_name]";
    public const string User1Id = "[user1_id]";
    public const string User2Id = "[user2_id]";
    public const string AcceptDate = "[accept_date]";
    public const string Currency = "[currency]";
    public const string Email = "[email]";
    public const string FriendshipStateName = "friendship_state_name";
    public const string FriendshipStateId = "friendship_state_id";

    public const string NickVar = "@nick";
    public const string LoginVar = "@login";
    public const string PasswordVar = "@password";
    public const string EmailVar = "@email";
    public const string NewLoginVar = "@new_login";
    public const string NewPasswordVar = "@new_password";
    public const string UserPictureIdVar = "@user_picture_id";
    public const string UserIdVar = "@user_id";
    public const string UserTypeNameVar = "@user_type_name";
    public const string User1IdVar = "@user1_id";
    public const string User2IdVar = "@user2_id";
    public const string UserTypeIdVar = "@user_type_id";
    public const string AdminTypeIdVar = "@admin_type_id";
    public const string RequestedTypeIdVar = "@req_type_id";
    public const string FriendshipStateNameVar = "@friendship_state";
    public const string AccessVar = "@access";
    public const string CurrencyVar = "@currency";

    public const string CreateUserProc = "[SP_CreateUser]";
    public const string SelectUserProc = "[SP_SelectUser]";
    public const string UpdateUserNickProc = "[SP_UpdateUserNick]";
    public const string UpdateUserLoginProc = "[SP_UpdateUserLogin]";
    public const string UpdateUserPasswordProc = "[SP_UpdateUserPassword]";
    public const string UpdateUserEmailProc = "[SP_UpdateUserEmail]";
    public const string SelectUserByNickProc = "[SP_SelectUserByNick]";
    public const string AuthenticateProc = "[SP_Authenticate]";

    public const string UpdateUserActivityProc = "[SP_UpdateUserActivity]";
    public const string CreateFriendshipProc = "[SP_CreateFriendship]";
    public const string GetUserTypeByTypeNameProc = "[SP_GetUserTypeByName]";
    public const string CheckAccessProc = "[SP_CheckAccess]";
    public const string GetFriendshipStateByNameProc = "[SP_GetFriendshipStateByName]";

    public const string SelectFriendChatIdProc = "[SP_SelectFriendChatId]";
    public const string SelectUserFriendshipProc = "[SP_SelectUserFrienship]";
    public const string UpdateUserPictureProc = "[SP_UpdateUserPictureProc]";

    public const string UpdateUserAnimationProc = "[SP_UpdateUserAnimation]";
    public const string UpdateUserCheckersProc = "[SP_UpdateUserCheckers]";

    public const string UserAddCheckersSkinProc = "[SP_UserAddCheckersSkin]";
    public const string UserAddAchievementProc = "[SP_UserAddAchievement]";
    public const string UserAddAnimationProc = "[SP_UserAddAnimationProc]";

    public const string UserBuyCheckersSkinProc = "[SP_UserBuyCheckersSkin]";
    public const string UserBuyAnimationProc = "[SP_UserBuyAnimation]";
    public const string UserBuyLootBoxProc = "[SP_UserBuyLootBox]";

    public const string SelectUserAchievementProc = "[SP_SelectUserAchievement]";
    public const string SelectUserCheckersSkinProc = "[SP_SelectUserCheckersSkin]";
    public const string SelectUserAnimationProc = "[SP_SelectUserAnimation]";

    public const int ValidAccess = 1;
    public const int InvalidAccess = -1;

    public const string UserAuthCondition = $"{Login}={LoginVar} AND {Password}={PasswordVar}";


    internal UserRepository(SqlConnection connection) : base(connection) { }

    private IEnumerable<int> GetUserI(int id, string name)
    {
        using var command = CreateProcedure(name);
        command.Parameters.Add(IdParameter(id));
        List<int> list = new();
        using var reader = command.ExecuteReader();
        while (reader.Read())
            list.Add(reader.GetFieldValue<int>(Id));
        return list;
    }

    private IEnumerable<int> GetUserAchievements(int id) =>
        GetUserI(id, SelectUserAchievementProc);

    private IEnumerable<int> GetUserAnimations(int id) =>
        GetUserI(id, SelectUserAnimationProc);

    private IEnumerable<int> GetUserCheckerSkins(int id) =>
        GetUserI(id, SelectUserCheckersSkinProc);


    public bool CreateUser(UserCreationData user)
    {
        using var command = CreateProcedure(CreateUserProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(user.Login),
                PasswordParameter(user.Password),
                new SqlParameter{ParameterName = NickVar,SqlDbType = SqlDbType.NVarChar,Value = user.Nick},
                new SqlParameter{ParameterName = EmailVar,SqlDbType = SqlDbType.NVarChar,Value = user.Email},
                new SqlParameter {ParameterName = UserTypeNameVar,SqlDbType = SqlDbType.NVarChar,Value = UserType.Player}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool DeleteUser(Credential credential)
    {
        throw new NotImplementedException();
    }

    public PublicUserData GetUser(int userId)
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
        user.Achievements = GetUserAchievements(userId);
        return user;
    }


    private int Auth(Credential credential)
    {
        using var command = CreateProcedureReturn(AuthenticateProc);
        command.Parameters.AddRange(new[]
        {
            LoginParameter(credential.Login),
            PasswordParameter(credential.Password)
        });
        command.ExecuteScalar();
        return command.GetReturn();
    }

    public User GetSelf(Credential credential)
    {
        var userId = Auth(credential);
        if (userId == InvalidId)
            return User.Invalid;
        var user = new User(GetUser(userId));
        using (var command = CreateProcedure(SelectUserFriendshipProc))
        {
            command.Parameters.Add(IdParameter(userId));
            using var reader = command.ExecuteReader();
            var list = new List<Friendship>();
            while (reader.Read())
                list.Add(reader.GetFriendship());
            user.Friends = list;
        }
        user.Animations = GetUserAnimations(userId);
        user.CheckerSkins = GetUserCheckerSkins(userId);
        user.Animations = GetUserAnimations(userId);
        return user;
    }

    public FriendUserData GetFriend(Credential credential, int friendId)
    {
        var userId = Auth(credential);
        if (userId == InvalidId)
            return FriendUserData.Invalid;

        var user = new FriendUserData(GetUser(userId)) {Achievements = GetUserAchievements(userId)};
        using var command = CreateProcedureReturn(SelectFriendChatIdProc);
        command.Parameters.AddRange(new[]
        {
            new SqlParameter {ParameterName = User1IdVar,SqlDbType = SqlDbType.Int,Value = userId},
            new SqlParameter {ParameterName = User2IdVar,SqlDbType = SqlDbType.Int,Value = friendId}
        });
        command.ExecuteNonQuery();
        user.ChatId = command.GetReturn();
        return user;
    }

    public bool SelectAnimation(Credential credential, int animationId)
    {
        using var command = CreateProcedure(UpdateUserAnimationProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(animationId)
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool SelectCheckers(Credential credential, int checkersId)
    {
        using var command = CreateProcedure(UpdateUserCheckersProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(checkersId)
            });
        return command.ExecuteNonQuery() > 0;
    }


    public bool Authenticate(Credential user)
    {
        return Auth(user) != InvalidId;
    }

    public bool UpdateUserNick(Credential credential, string nick)
    {
        using var command = CreateProcedure(UpdateUserNickProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                new SqlParameter{ParameterName = NickVar,SqlDbType = SqlDbType.NVarChar,Value = nick}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateUserLogin(Credential credential, string login)
    {
        using var command = CreateProcedure(UpdateUserLoginProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                new SqlParameter{ParameterName = NewLoginVar,SqlDbType = SqlDbType.NVarChar,Value = login}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateUserPassword(Credential credential, string password)
    {
        using var command = CreateProcedure(UpdateUserPasswordProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                new SqlParameter{ParameterName = NewPasswordVar,SqlDbType = SqlDbType.NVarChar,Value = password}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateUserEmail(Credential credential, string email)
    {
        using var command = CreateProcedure(UpdateUserEmailProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                new SqlParameter{ParameterName = EmailVar,SqlDbType = SqlDbType.NVarChar,Value = email}
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateUserPicture(Credential credential, int pictureId)
    {
        using var command = CreateProcedure(UpdateUserPictureProc);
        command.Parameters.AddRange(new[]
        {
            LoginParameter(credential.Login),
            PasswordParameter(credential.Password),
            IdParameter(pictureId)
        });
        return command.ExecuteNonQuery() > 0;
    }

    public IEnumerable<PublicUserData> GetUsersByNick(string pattern)
    {
        using var command = CreateProcedure(SelectUserByNickProc);
        command.Parameters.AddRange(
            new[]
            {
                new SqlParameter{ParameterName = NickVar, SqlDbType = SqlDbType.NVarChar, Value = pattern}
            });
        List<PublicUserData> list = new();
        using (var reader = command.ExecuteReader())
        {

            while (reader.Read())
            {
                var user = reader.GetUser();
                list.Add(new PublicUserData(user));
            }
        }

        foreach (var user in list)
            user.Achievements = GetUserAchievements(user.Id);

        return list;
    }

    public bool AddFriend(Credential credential, int userId)
    {
        using var command = CreateProcedure(CreateFriendshipProc);
        command.Parameters.AddRange(
            new[]
            {
                LoginParameter(credential.Login),
                PasswordParameter(credential.Password),
                IdParameter(userId)
            });
        return command.ExecuteNonQuery() > 0;
    }

    public bool DeleteFriend(Credential credential, int userId)
    {
        throw new NotImplementedException();
    }

    public bool AcceptFriend(Credential credential, int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> GetAvailableAnimations(Credential c)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> GetAvailableCheckers(Credential c)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> GetAvailableLootBoxes(Credential c)
    {
        throw new NotImplementedException();
    }

    public bool BuyCheckersSkin(Credential credential, int id)
    {
        throw new NotImplementedException();
    }

    public bool BuyAnimation(Credential credential, int id)
    {
        throw new NotImplementedException();
    }

    public bool BuyLootBox(Credential credential, int id)
    {
        throw new NotImplementedException();
    }
}