using System.Collections.Generic;
using Common.Entity;

namespace WebService.Repository.Interface;

public interface IUserRepository
{
    bool CreateUser(UserCreationData user);
    bool DeleteUser(Credential credential);
    PublicUserData GetUser(int userId);
    User GetSelf(Credential credential);
    FriendUserData GetFriend(Credential credential, int friendId);

    bool SelectAnimation(Credential credential, int animationId);
    bool SelectCheckers(Credential credential, int checkersId);

    bool BuyCheckersSkin(Credential credential, int id);
    bool BuyAnimation(Credential credential, int id);
    bool BuyLootBox(Credential credential, int id);


    bool Authenticate(Credential user);
    bool UpdateUserNick(Credential credential, string nick);
    bool UpdateUserLogin(Credential credential, string login);
    bool UpdateUserPassword(Credential credential, string password);
    bool UpdateUserEmail(Credential credential, string email);
    bool UpdateUserPicture(Credential credential, int pictureId);

    IEnumerable<PublicUserData> GetUsersByNick(string pattern);
    bool AddFriend(Credential credential, int userId);
    bool DeleteFriend(Credential credential, int userId);
    bool AcceptFriend(Credential credential, int userId);

    IEnumerable<int> GetAvailableAnimations(Credential c);
    IEnumerable<int> GetAvailableCheckers(Credential c);
    IEnumerable<int> GetAvailableLootBoxes(Credential c);
}