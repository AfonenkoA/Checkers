using Common.Entity;
using GameModel;

namespace Api.Interface;

public interface IAsyncUserApi
{
    //User
    Task<bool> CreateUser(UserCreationData user);
    Task<bool> DeleteUser(ICredential credential);
    Task<(bool, PublicUserData)> TryGetUser(int id);
    Task<(bool, User)> TryGetSelf(ICredential credential);
    Task<(bool, FriendUserData)> TryGetFriend(ICredential credential, int id);
    Task<(bool, IEnumerable<GameInfo>)> TryGetGames(ICredential credential);
    //User Item Activities
    Task<bool> SelectAnimation(ICredential credential, int id);
    Task<bool> SelectCheckers(ICredential credential, int id);

    Task<bool> BuyCheckersSkin(ICredential credential, int id);
    Task<bool> BuyAnimation(ICredential credential, int id);
    Task<bool> BuyLootBox(ICredential credential, int id);

    //User Account Activities 
    Task<bool> Authenticate(ICredential user);
    Task<bool> UpdateUserNick(ICredential credential, string nick);
    Task<bool> UpdateUserLogin(ICredential credential, string login);
    Task<bool> UpdateUserPassword(ICredential credential, string password);
    Task<bool> UpdateUserEmail(ICredential credential, string email);
    Task<bool> UpdateUserPicture(ICredential credential, int id);

    //Friends
    Task<(bool, IEnumerable<PublicUserData>)> TryGetUsersByNick(string pattern);
    Task<bool> AddFriend(ICredential credential, int id);
    Task<bool> DeleteFriend(ICredential credential, int id);
    Task<bool> AcceptFriend(ICredential credential, int id);
}