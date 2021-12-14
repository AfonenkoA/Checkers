using Common.Entity;
using GameModel;

namespace Api.Interface;

public interface IAsyncUserApi
{
    //User
    Task<bool> CreateUser(UserCreationData user);
    Task<bool> DeleteUser(Credential credential);
    Task<(bool, PublicUserData)> TryGetUser(int id);
    Task<(bool, User)> TryGetSelf(Credential credential);
    Task<(bool, FriendUserData)> TryGetFriend(Credential credential, int id);
    Task<(bool, IEnumerable<GameInfo>)> TryGetGames(Credential credential);
    //User Item Activities
    Task<bool> SelectAnimation(Credential credential, int id);
    Task<bool> SelectCheckers(Credential credential, int id);

    Task<bool> BuyCheckersSkin(Credential credential, int id);
    Task<bool> BuyAnimation(Credential credential, int id);
    Task<bool> BuyLootBox(Credential credential, int id);

    //User Account Activities 
    Task<bool> Authenticate(Credential user);
    Task<bool> UpdateUserNick(Credential credential, string nick);
    Task<bool> UpdateUserLogin(Credential credential, string login);
    Task<bool> UpdateUserPassword(Credential credential, string password);
    Task<bool> UpdateUserEmail(Credential credential, string email);
    Task<bool> UpdateUserPicture(Credential credential, int id);

    //Friends
    Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern);
    Task<bool> AddFriend(Credential credential, int id);
    Task<bool> DeleteFriend(Credential credential, int id);
    Task<bool> AcceptFriend(Credential credential, int id);
}