using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncUserApi
{
    //User
    Task<bool> CreateUser(UserCreationData user);
    Task<bool> DeleteUser(Credential credential);
    Task<(bool,PublicUserData)> TryGetUser(int userId);
    Task<(bool,User)> TryGetSelf(Credential credential);
    Task<(bool,FriendUserData)> TryGetFriend(Credential credential, int friendId);

    //User Item Activities
    Task<bool> SelectAnimation(Credential credential, int animationId);
    Task<bool> SelectCheckers(Credential credential, int checkersId);
    Task<bool> BuyItem(Credential credential, int itemId);


    //User Account Activities 
    Task<bool> Authenticate(Credential user);
    Task<bool> UpdateUserNick(Credential credential, string nick);
    Task<bool> UpdateUserLogin(Credential credential, string login);
    Task<bool> UpdateUserPassword(Credential credential, string password);
    Task<bool> UpdateUserEmail(Credential credential, string email);

    //Friends
    Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern);
    Task<bool> AddFriend(Credential credential, int userId);
    Task<bool> DeleteFriend(Credential credential, int userId);
    Task<bool> AcceptFriend(Credential credential, int userId);
}