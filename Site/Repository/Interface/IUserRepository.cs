using Common.Entity;
using Site.Data.Models.User;

namespace Site.Repository.Interface;

public interface IUserRepository
{
    
    public Task<(bool, IEnumerable<UserInfo>)> GetByNick(string pattern);
    public Task<bool> Authorize(Credential credential);
    public Task<(bool, Self)> GetSelf(Credential credential);
    public Task<(bool, UserInfo)> GetUser(int id);
    public Task<(bool, IDictionary<long, UserInfo>)> GetTop();
    public Task<(bool, IDictionary<long, UserInfo>)> GetTop(Credential credential);

    Task<bool> UpdateUserNick(Credential credential, string nick);
    Task<bool> UpdateUserLogin(Credential credential, string login);
    Task<bool> UpdateUserPassword(Credential credential, string password);
    Task<bool> UpdateUserEmail(Credential credential, string email);
    Task<bool> UpdateUserPicture(Credential credential, int id);
}