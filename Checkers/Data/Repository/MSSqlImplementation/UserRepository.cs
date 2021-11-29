using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.ItemRepository;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class UserRepository : Repository, IUserRepository
{
    public const string UserTable = "[User]";
    public const string UserItemTable = "[UserItem]";
    public const string UserItemExtendedView = "[UserItemExtended]";
    public const string UserTypeTable = "[UserType]";
    public const string FriendshipStateTable = "[FriendshipState]";
    public const string FriendshipTable = "[Friendship]";

    public const string Nick = "[nick]";
    public const string Login = "[login]";
    public const string Password = "[password]";
    public const string SocialCredit = "[social_credit]";
    public const string LastActivity = "[last_activity]";
    public const string PictureId = "[picture_id]";
    public const string CheckersId = "[checkers_id]";
    public const string AnimationId = "[animation_id]";
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
    public const string SelectUserItemProc = "[SP_SelectUserItem]";
    public const string SelectUserProc = "[SP_SelectUser]";
    public const string UpdateUserNickProc = "[SP_UpdateUserNick]";
    public const string UpdateUserLoginProc = "[SP_UpdateUserLogin]";
    public const string UpdateUserPasswordProc = "[SP_UpdateUserPassword]";
    public const string UpdateUserEmailProc = "[SP_UpdateUserEmail]";
    public const string SelectUserByNickProc = "[SP_SelectUserByNick]";
    public const string AuthenticateProc = "[SP_Authenticate]";
    public const string UpdateUserAnimationProc = "[SP_UpdateUserAnimation]";
    public const string UpdateUserCheckersProc = "[SP_UpdateUserCheckers]";
    public const string UpdateUserActivityProc = "[SP_UpdateUserActivity]";
    public const string CreateFriendshipProc = "[SP_CreateFriendship]";
    public const string GetUserTypeByTypeNameProc = "[SP_GetUserTypeByName]";
    public const string CheckAccessProc = "[SP_CheckAccess]";
    public const string GetFriendshipStateByNameProc = "[SP_GetFriendshipStateByName]";
    public const string UserAddItemProc = "[SP_AddUserItem]";
    public const string UserBuyItemProc = "[SP_UserBuyItem]";
    public const string SelectAllUserItemProc = "[SP_SelectAllUserItem]";
    public const string SelectFriendChatIdProc = "[SP_SelectFriendChatId]";
    public const string SelectUserFriendshipProc = "[SP_SelectUserFrienship]";
    public const string UpdateUserPictureProc = "[SP_UpdateUserPictureProc]";


    public const int ValidAccess = 1;
    public const int InvalidAccess = -1;

    public const string UserAuthCondition = $"{Login}={LoginVar} AND {Password}={PasswordVar}";


    internal UserRepository(SqlConnection connection) : base(connection) { }

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
        user.Achievements = GetUserItems(userId, ItemType.Achievement);
        return user;
    }

    private IEnumerable<int> GetUserItems(int id, ItemType type)
    {
        using var command = CreateProcedure(SelectUserItemProc);
        command.Parameters.AddRange(new[]
        {
            IdParameter(id),
            new SqlParameter{ParameterName = ItemTypeVar,SqlDbType = SqlDbType.NVarChar,Value = type}
        });
        using var reader = command.ExecuteReader();
        List<int> items = new();
        if (reader.Read())
            items.Add(reader.GetFieldValue<int>(Id));
        return items;
    }

    private int Auth(Credential credential)
    {
        using var command = CreateProcedure(AuthenticateProc);
        command.Parameters.AddRange(new[]
        {
            LoginParameter(credential.Login),
            PasswordParameter(credential.Password)
        });
        command.ExecuteNonQuery();
        return command.GetReturn();
    }

    public User GetSelf(Credential credential)
    {
        var userId = Auth(credential);
        if (userId == InvalidId)
            return User.Invalid;

        var user = new User(GetUser(userId));
        using (var command = CreateProcedure(SelectAllUserItemProc))
        {
            command.Parameters.Add(IdParameter(userId));
            using var reader = command.ExecuteReader();
            var list = new List<int>();
            while (reader.Read())
                list.Add(reader.GetFieldValue<int>(ItemId));
            user.Items = list;
        }
        using (var command = CreateProcedure(SelectUserFriendshipProc))
        {
            command.Parameters.Add(IdParameter(userId));
            using var reader = command.ExecuteReader();
            var list = new List<Friendship>();
            while (reader.Read())
                list.Add(reader.GetFriendship());
            user.Friends = list;
        }
        return user;
    }

    public FriendUserData GetFriend(Credential credential, int friendId)
    {
        var userId = Auth(credential);
        if (userId == InvalidId)
            return FriendUserData.Invalid;

        var user = new FriendUserData(GetUser(userId))
        {
            Achievements = GetUserItems(userId, ItemType.Achievement)
        };
        using var command = CreateProcedure(SelectFriendChatIdProc);
        command.Parameters.AddRange(new []
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

    public bool BuyItem(Credential credential, int itemId)
    {
        using var command = CreateProcedure(UserBuyItemProc);
        command.Parameters.AddRange(new[]
        {
            LoginParameter(credential.Login),
            PasswordParameter(credential.Password),
            IdParameter(itemId)
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
        throw new NotImplementedException();
    }

    public IEnumerable<PublicUserData> GetUsersByNick(string pattern)
    {
        using var command = CreateProcedure(SelectUserByNickProc);
        command.Parameters.AddRange(
            new[]
            {
                new SqlParameter{ParameterName = NickVar, SqlDbType = SqlDbType.NVarChar, Value = pattern}
            });
        using var reader = command.ExecuteReader();
        List<PublicUserData> list = new();
        while (reader.Read())
        {
            var user = reader.GetUser();
            list.Add(new PublicUserData(user)
            {
                Achievements = GetUserItems(user.Id, ItemType.Achievement)
            });
        }
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
}