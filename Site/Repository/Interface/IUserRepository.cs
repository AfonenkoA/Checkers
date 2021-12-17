using Common.Entity;
using Site.Data.Models.User;

namespace Site.Repository.Interface;

public interface IUserRepository
{
    
    public Task<(bool, IEnumerable<UserInfo>)> GetByNick(string pattern);
    public Task<bool> Authorize(ICredential credential);
    public Task<(bool, Self)> GetSelf(ICredential credential);
    public Task<(bool, UserInfo)> GetUser(int id);
    public Task<(bool, IDictionary<long, UserInfo>)> GetTop();
    public Task<(bool, IDictionary<long, UserInfo>)> GetTop(ICredential credential);

    Task<bool> UpdateUserNick(ICredential credential, string nick);
    Task<bool> UpdateUserLogin(ICredential credential, string login);
    Task<bool> UpdateUserPassword(ICredential credential, string password);
    Task<bool> UpdateUserEmail(ICredential credential, string email);
    Task<bool> UpdateUserPicture(ICredential credential, int id);
}