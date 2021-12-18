using Common.Entity;
using WinFormsClient.Model;

namespace WinFormsClient.Repository.Interface;

public interface IUserRepository
{
    public Task<(bool, Self)> GetSelf(ICredential c);
    public Task<(bool, Collection)> GetCollection(ICredential c);
    public Task<bool> SelectAnimation(ICredential credential, int id);
    public Task<bool> SelectCheckers(ICredential credential, int id);
    public Task<bool> BuyCheckersSkin(ICredential credential, int id);
    public Task<bool> BuyAnimation(ICredential credential, int id);
}