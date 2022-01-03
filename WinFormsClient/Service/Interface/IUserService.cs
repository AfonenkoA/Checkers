using Common.Entity;
using WinFormsClient.Model;

namespace WinFormsClient.Service.Interface;

public interface IUserService
{
    public Task<(bool, Model.User)> GetUser(int id);
    public Task<(bool, Self)> GetSelf(ICredential c);
    public Task<(bool, Collection)> GetCollection(ICredential c);
    public Task<(bool, Shop)> GetShop(ICredential c);
    public Task<bool> SelectAnimation(ICredential credential, int id);
    public Task<bool> SelectCheckers(ICredential credential, int id);
    public Task<bool> BuyCheckersSkin(ICredential credential, int id);
    public Task<bool> BuyAnimation(ICredential credential, int id);
    public Task<bool> BuyLootBox(ICredential credential, int viewLootBoxId);
}